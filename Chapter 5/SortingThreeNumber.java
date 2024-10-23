import java.util.Scanner;

public class SortingThreeNumber {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.print("Enter the first number: ");
        int num1 = scanner.nextInt();
        System.out.print("Enter the second number: ");
        int num2 = scanner.nextInt();
        System.out.print("Enter the third number: ");
        int num3 = scanner.nextInt();
        scanner.close();
        if (num1 <= num2 && num2 <= num3) {
            System.out.println("Numbers in ascending order: " + num1 + " " + num2 + " " + num3);
        } else if (num1 >= num2 && num2 >= num3) {
            System.out.println("Numbers in descending order: " + num1 + " " + num2 + " " + num3);
        } else {
            int temp;
            if (num1 > num2) {
                temp = num1;
                num1 = num2;
                num2 = temp;
            }
            if (num2 > num3) {
                temp = num2;
                num2 = num3;
                num3 = temp;
            }
            System.out.println("Numbers in ascending order: " + num1 + " " + num2 + " " + num3);
        }
    }
}