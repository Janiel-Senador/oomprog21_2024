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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // No DataGridView to configure
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

            string filePath = "student_record.txt";

            if (!File.Exists(filePath))
            {
                MessageBox.Show("File does not exist.");
                return;
            }

            var lines = File.ReadAllLines(filePath).ToList();
            int originalCount = lines.Count;
            lines = lines.Where(line => !line.Contains($"ID Number: {idToDelete},")).ToList();

            if (lines.Count == originalCount)
            {
                MessageBox.Show("No record found with that ID.");
            }
            else
            {
                File.WriteAllLines(filePath, lines);
                MessageBox.Show("Record deleted successfully.");
                ClearForm();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
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

            string filePath = "student_record.txt";

            if (!File.Exists(filePath))
            {
                MessageBox.Show("File does not exist.");
                return;
            }

            var lines = File.ReadAllLines(filePath).ToList();
            bool recordUpdated = false;

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains($"ID Number: {idNumber},"))
                {
                    string updatedRecord = $"ID Number: {idNumber}, " +
                                           $"First Name: {firstName}, " +
                                           $"Last Name: {lastName}, " +
                                           $"Middle Name: {middleName}, " +
                                           $"Course: {course}, " +
                                           $"Year Level: {yearLevel}, " +
                                           $"Birthday: {birthday}";

                    lines[i] = updatedRecord;
                    recordUpdated = true;
                    break;
                }
            }

            if (recordUpdated)
            {
                File.WriteAllLines(filePath, lines);
                MessageBox.Show("Record updated successfully.");
                ClearForm();
            }
            else
            {
                MessageBox.Show("No record found with that ID.");
            }
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
            return DateTime.TryParse(birthday, out _);
        }

        // Optional: Empty text changed events if needed by designer
        private void txtIDNumber_TextChanged(object sender, EventArgs e) { }
        private void txtFirstName_TextChanged(object sender, EventArgs e) { }
        private void txtLastName_TextChanged(object sender, EventArgs e) { }
        private void txtMiddleName_TextChanged(object sender, EventArgs e) { }
        private void txtCourse_TextChanged(object sender, EventArgs e) { }
        private void txtYearLevel_TextChanged(object sender, EventArgs e) { }
        private void dtpBirthday_ValueChanged(object sender, EventArgs e) { }
    }
}



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
