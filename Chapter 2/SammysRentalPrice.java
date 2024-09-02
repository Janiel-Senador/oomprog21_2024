import java.util.Scanner;

public class SammysRentalPrice {
    public static void main(String[] args) {
        final double RATE_PER_HOUR = 40.0;
        final double RATE_PER_MINUTE = 1.0 / 60.0;
        
        Scanner scanner = new Scanner(System.in);
        System.out.print("Enter the number of minutes rented: ");
        int minutes = scanner.nextInt();
        
        int hours = minutes / 60;
        int remainingMinutes = minutes % 60;
        
        double totalPrice = (hours * RATE_PER_HOUR) + (remainingMinutes * RATE_PER_MINUTE);
        
        System.out.println("SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
        System.out.println("S  Sammy's make it fun in the sun. S");
        System.out.println("SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");

        System.out.println("Hours: " + hours);
        System.out.println("Minutes: " + remainingMinutes);
        System.out.println("Total Price: $" + totalPrice);
    }
}