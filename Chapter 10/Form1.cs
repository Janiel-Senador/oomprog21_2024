using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SENADOR_SemiFinalActivity2
{
    public partial class Form1 : Form
    {
        private readonly string filePath = "student_record.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadStudentRecordsToTable();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string idNumber = txtIDNumber.Text.Trim();
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string middleName = txtMiddleName.Text.Trim();
            string course = txtCourse.Text.Trim();
            string yearLevel = txtYearLevel.Text.Trim();
            string birthday = dtpBirthday.Value.ToString("yyyy/MM/dd");

            if (!ValidateIDNumber(idNumber))
            {
                MessageBox.Show("ID Number must be numeric.");
                return;
            }
            if (!ValidateFirstName(firstName))
            {
                MessageBox.Show("First Name cannot contain special characters.");
                return;
            }
            if (!ValidateYearLevel(yearLevel))
            {
                MessageBox.Show("Year Level must be numeric.");
                return;
            }

            string studentRecord = $"ID:{idNumber}|FirstName:{firstName}|LastName:{lastName}|MiddleName:{middleName}|Course:{course}|YearLevel:{yearLevel}|Birthday:{birthday}";

            try
            {
                File.AppendAllText(filePath, studentRecord + Environment.NewLine);
                MessageBox.Show("Student record saved successfully!");
                ClearForm();
                LoadStudentRecordsToTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string idToDelete = txtIDNumber.Text.Trim();

            if (!ValidateIDNumber(idToDelete))
            {
                MessageBox.Show("Please enter a valid numeric ID.");
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show("No records found.");
                return;
            }

            var lines = File.ReadAllLines(filePath);
            bool found = false;

            var updatedLines = lines.Where(line =>
            {
                var match = Regex.Match(line, @"ID:(\d+)\|");
                if (match.Success && match.Groups[1].Value == idToDelete)
                {
                    found = true;
                    return false; // skip this line
                }
                return true; // keep the line
            }).ToList();

            if (found)
            {
                File.WriteAllLines(filePath, updatedLines);
                MessageBox.Show("Record deleted successfully.");
                ClearForm();
                LoadStudentRecordsToTable();
            }
            else
            {
                MessageBox.Show("Record not found.");
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string idToUpdate = txtIDNumber.Text.Trim();

            if (!ValidateIDNumber(idToUpdate))
            {
                MessageBox.Show("Please enter a valid numeric ID.");
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show("No records found.");
                return;
            }

            var lines = File.ReadAllLines(filePath).ToList();
            bool found = false;

            for (int i = 0; i < lines.Count; i++)
            {
                var match = Regex.Match(lines[i], @"ID:(\d+)\|");
                if (match.Success && match.Groups[1].Value == idToUpdate)
                {
                    // Parse the current values
                    var existingRecord = ParseRecord(lines[i]);

                    string firstName = string.IsNullOrWhiteSpace(txtFirstName.Text) ? existingRecord[1] : txtFirstName.Text.Trim();
                    string lastName = string.IsNullOrWhiteSpace(txtLastName.Text) ? existingRecord[2] : txtLastName.Text.Trim();
                    string middleName = string.IsNullOrWhiteSpace(txtMiddleName.Text) ? existingRecord[3] : txtMiddleName.Text.Trim();
                    string course = string.IsNullOrWhiteSpace(txtCourse.Text) ? existingRecord[4] : txtCourse.Text.Trim();
                    string yearLevel = string.IsNullOrWhiteSpace(txtYearLevel.Text) ? existingRecord[5] : txtYearLevel.Text.Trim();
                    string birthday = dtpBirthday.Value == DateTime.Now ? existingRecord[6] : dtpBirthday.Value.ToString("yyyy/MM/dd");

                    string updatedRecord = $"ID:{idToUpdate}|FirstName:{firstName}|LastName:{lastName}|MiddleName:{middleName}|Course:{course}|YearLevel:{yearLevel}|Birthday:{birthday}";

                    lines[i] = updatedRecord;
                    found = true;
                    break;
                }
            }

            if (found)
            {
                File.WriteAllLines(filePath, lines);
                MessageBox.Show("Record updated successfully.");
                ClearForm();
                LoadStudentRecordsToTable();
            }
            else
            {
                MessageBox.Show("Record not found.");
            }
        }



        // === Load to Table ===
        private void LoadStudentRecordsToTable()
        {
            dataGridViewRecords.Rows.Clear();
            dataGridViewRecords.Columns.Clear();

            dataGridViewRecords.Columns.Add("ID", "ID");
            dataGridViewRecords.Columns.Add("FirstName", "First Name");
            dataGridViewRecords.Columns.Add("LastName", "Last Name");
            dataGridViewRecords.Columns.Add("MiddleName", "Middle Name");
            dataGridViewRecords.Columns.Add("Course", "Course");
            dataGridViewRecords.Columns.Add("YearLevel", "Year Level");
            dataGridViewRecords.Columns.Add("Birthday", "Birthday");

            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var record = ParseRecord(line);
                    if (record != null)
                    {
                        dataGridViewRecords.Rows.Add(record);
                    }
                }
            }
        }

        private string[] ParseRecord(string line)
        {
            try
            {
                string[] fields = line.Split('|');
                string[] values = new string[7];

                foreach (string field in fields)
                {
                    var kvp = field.Split(':');
                    switch (kvp[0])
                    {
                        case "ID": values[0] = kvp[1]; break;
                        case "FirstName": values[1] = kvp[1]; break;
                        case "LastName": values[2] = kvp[1]; break;
                        case "MiddleName": values[3] = kvp[1]; break;
                        case "Course": values[4] = kvp[1]; break;
                        case "YearLevel": values[5] = kvp[1]; break;
                        case "Birthday": values[6] = kvp[1]; break;
                    }
                }

                return values;
            }
            catch
            {
                return null;
            }
        }

        // === Validation Helpers ===
        private bool ValidateIDNumber(string idNumber) => Regex.IsMatch(idNumber, @"^\d+$");
        private bool ValidateFirstName(string firstName) => Regex.IsMatch(firstName, @"^[a-zA-Z- ]+$");
        private bool ValidateYearLevel(string yearLevel) => Regex.IsMatch(yearLevel, @"^\d+$");

        private void ClearForm()
        {
            txtIDNumber.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtMiddleName.Clear();
            txtCourse.Clear();
            txtYearLevel.Clear();
            dtpBirthday.Value = DateTime.Now;
        }

        // Optional event hooks
        private void txtIDNumber_TextChanged(object sender, EventArgs e) { }
        private void txtFirstName_TextChanged(object sender, EventArgs e) { }
        private void txtLastName_TextChanged(object sender, EventArgs e) { }
        private void txtMiddleName_TextChanged(object sender, EventArgs e) { }
        private void txtCourse_TextChanged(object sender, EventArgs e) { }
        private void txtYearLevel_TextChanged(object sender, EventArgs e) { }
        private void dtpBirthday_ValueChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}




/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace VERIFY
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string idNumber = txtIDNumber.Text;
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string middleName = txtMiddleName.Text;
            string course = txtCourse.Text;
            string yearLevel = txtYearLevel.Text;
            string birthday = dtpBirthday.Value.ToString("yyyy/MM/dd");

            if (!ValidateIDNumber(idNumber))
            {
                MessageBox.Show("ID Number must be numeric.");
                return;
            }
            if (!ValidateFirstName(firstName))
            {
                MessageBox.Show("First Name cannot contain special characters.");
                return;
            }
            if (!ValidateYearLevel(yearLevel))
            {
                MessageBox.Show("Year Level must be numeric.");
                return;
            }
            if (!ValidateBirthday(birthday))
            {
                MessageBox.Show("Invalid Birthday format.");
                return;
            }

            string studentRecord = $"ID Number: {idNumber}, " +
                                   $"First Name: {firstName}, " +
                                   $"Last Name: {lastName}, " +
                                   $"Middle Name: {middleName}, " +
                                   $"Course: {course}, " +
                                   $"Year Level: {yearLevel}, " +
                                   $"Birthday: {birthday}";

            try
            {
                File.AppendAllText("student_record.txt", studentRecord + Environment.NewLine);
                MessageBox.Show("Student record saved successfully!");
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string idToDelete = txtIDNumber.Text;

            if (!ValidateIDNumber(idToDelete))
            {
                MessageBox.Show("Please enter a valid numeric ID to delete.");
                return;
            }

            if (!File.Exists("student_record.txt"))
            {
                MessageBox.Show("No records found.");
                return;
            }

            string[] lines = File.ReadAllLines("student_record.txt");
            bool found = false;

            List<string> updatedLines = new List<string>();

            foreach (string line in lines)
            {
                if (!line.Contains($"ID Number: {idToDelete},"))
                {
                    updatedLines.Add(line);
                }
                else
                {
                    found = true;
                }
            }

            if (found)
            {
                File.WriteAllLines("student_record.txt", updatedLines);
                MessageBox.Show("Record deleted successfully!");
                ClearForm();
            }
            else
            {
                MessageBox.Show("Record not found.");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string idToUpdate = txtIDNumber.Text;

            if (!ValidateIDNumber(idToUpdate))
            {
                MessageBox.Show("Please enter a valid numeric ID to update.");
                return;
            }

            if (!File.Exists("student_record.txt"))
            {
                MessageBox.Show("No records found.");
                return;
            }

            string[] lines = File.ReadAllLines("student_record.txt");
            bool found = false;

            string updatedRecord = $"ID Number: {txtIDNumber.Text}, " +
                                   $"First Name: {txtFirstName.Text}, " +
                                   $"Last Name: {txtLastName.Text}, " +
                                   $"Middle Name: {txtMiddleName.Text}, " +
                                   $"Course: {txtCourse.Text}, " +
                                   $"Year Level: {txtYearLevel.Text}, " +
                                   $"Birthday: {dtpBirthday.Value.ToString("yyyy/MM/dd")}";

            List<string> updatedLines = new List<string>();

            foreach (string line in lines)
            {
                if (line.Contains($"ID Number: {idToUpdate},"))
                {
                    updatedLines.Add(updatedRecord);
                    found = true;
                }
                else
                {
                    updatedLines.Add(line);
                }
            }

            if (found)
            {
                File.WriteAllLines("student_record.txt", updatedLines);
                MessageBox.Show("Record updated successfully!");
                ClearForm();
            }
            else
            {
                MessageBox.Show("Record not found.");
            }
        }

        // === Validation Helpers ===
        private bool ValidateIDNumber(string idNumber)
        {
            return Regex.IsMatch(idNumber, @"^\d+$");
        }

        private bool ValidateFirstName(string firstName)
        {
            return Regex.IsMatch(firstName, @"^[a-zA-Z- ]+$");
        }

        private bool ValidateYearLevel(string yearLevel)
        {
            return Regex.IsMatch(yearLevel, @"^\d+$");
        }

        private bool ValidateBirthday(string birthday)
        {
            DateTime parsedDate;
            return DateTime.TryParse(birthday, out parsedDate);
        }

        private void ClearForm()
        {
            txtIDNumber.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtMiddleName.Clear();
            txtCourse.Clear();
            txtYearLevel.Clear();
            dtpBirthday.Value = DateTime.Now;
        }

        // === TextChanged Event Placeholders ===
        private void txtIDNumber_TextChanged(object sender, EventArgs e) { }
        private void txtFirstName_TextChanged(object sender, EventArgs e) { }
        private void txtLastName_TextChanged(object sender, EventArgs e) { }
        private void txtMiddleName_TextChanged(object sender, EventArgs e) { }
        private void txtCourse_TextChanged(object sender, EventArgs e) { }
        private void txtYearLevel_TextChanged(object sender, EventArgs e) { }
        private void dtpBirthday_ValueChanged(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
*/

/*using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Net;


namespace SENADOR_SemiFinalActivity2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string idNumber = txtIDNumber.Text;
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string middleName = txtMiddleName.Text;
            string course = txtCourse.Text;
            string yearLevel = txtYearLevel.Text;
            string birthday = dtpBirthday.Value.ToString("yyyy/MM/dd");

            if (!ValidateIDNumber(idNumber))
            {
                MessageBox.Show("ID Number must be numeric.");
                return;
            }
            if (!ValidateFirstName(firstName))
            {
                MessageBox.Show("First Name cannot contain special characters.");
                return;
            }
            if (!ValidateYearLevel(yearLevel))
            {
                MessageBox.Show("Year Level must be numeric.");
                return;
            }
            if (!ValidateBirthday(birthday))
            {
                MessageBox.Show("Invalid Birthday format.");
                return;
            }
            string studentRecord = $"ID Number: {idNumber}, " +
                           $"First Name: {firstName}, " +
                           $"Last Name: {lastName}, " +
                           $"Middle Name: {middleName}, " +
                           $"Course: {course}, " +
                           $"Year Level: {yearLevel}, " +
                           $"Birthday: {birthday}";

            //string studentRecord = $"{idNumber},{firstName},{lastName},{middleName},{course},{yearLevel},{birthday}";

            try
            {
                File.AppendAllText("student_record.txt", studentRecord + Environment.NewLine);
                MessageBox.Show("Student record saved successfully!");
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}");
            }
        
    }
        private bool ValidateIDNumber(string idNumber)
        {
            return Regex.IsMatch(idNumber, @"^\d+$");
        }

        private bool ValidateFirstName(string firstName)
        {
            return Regex.IsMatch(firstName, @"^[a-zA-Z- ]+$");
        }

        private bool ValidateYearLevel(string yearLevel)
        {
            return Regex.IsMatch(yearLevel, @"^\d+$");
        }

        private bool ValidateBirthday(string birthday)
        {
            DateTime parsedDate;
            return DateTime.TryParse(birthday, out parsedDate);
        }

        private void ClearForm()
        {
            txtIDNumber.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtMiddleName.Clear();
            txtCourse.Clear();
            txtYearLevel.Clear();
            dtpBirthday.Value = DateTime.Now;
        }
        private void btnDelete_Click(object sender, EventArgs e)

        {
            string idToDelete = txtIDNumber.Text;

            if (!ValidateIDNumber(idToDelete))
            {
                MessageBox.Show("Please enter a valid numeric ID to delete.");
                return;
            }

            string[] lines = File.ReadAllLines("student_record.txt");
            bool found = false;

            List<string> updatedLines = new List<string>();

            foreach (string line in lines)
            {
                if (!line.Contains($"ID Number: {idToDelete},"))
                {
                    updatedLines.Add(line);
                }
                else
                {
                    found = true;
                }
            }

            if (found)
            {
                File.WriteAllLines("student_record.txt", updatedLines);
                MessageBox.Show("Record deleted successfully!");
                ClearForm();
            }
            else
            {
                MessageBox.Show("Record not found.");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string idToUpdate = txtIDNumber.Text;

            if (!ValidateIDNumber(idToUpdate))
            {
                MessageBox.Show("Please enter a valid numeric ID to update.");
                return;
            }

            string[] lines = File.ReadAllLines("student_record.txt");
            bool found = false;

            List<string> updatedLines = new List<string>();

            string updatedRecord = $"ID Number: {txtIDNumber.Text}, " +
                                   $"First Name: {txtFirstName.Text}, " +
                                   $"Last Name: {txtLastName.Text}, " +
                                   $"Middle Name: {txtMiddleName.Text}, " +
                                   $"Course: {txtCourse.Text}, " +
                                   $"Year Level: {txtYearLevel.Text}, " +
                                   $"Birthday: {dtpBirthday.Value.ToString("yyyy/MM/dd")}";

            foreach (string line in lines)
            {
                if (line.Contains($"ID Number: {idToUpdate},"))
                {
                    updatedLines.Add(updatedRecord);
                    found = true;
                }
                else
                {
                    updatedLines.Add(line);
                }
            }

            if (found)
            {
                File.WriteAllLines("student_record.txt", updatedLines);
                MessageBox.Show("Record updated successfully!");
                ClearForm();
            }
            else
            {
                MessageBox.Show("Record not found.");
            }
        }


        private void txtIDNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMiddleName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCourse_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtYearLevel_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpBirthday_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}*/
