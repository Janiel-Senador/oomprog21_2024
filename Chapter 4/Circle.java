public class Circle {
    static double radius;
    static double diameter;
    static double area;

    public Circle() {
        radius = 1;
        diameter = radius * 2;
        area = Math.PI * radius * radius;
    }

    public void setRadius(double rad) {
        radius = rad;
        diameter = radius * 2;
        area = Math.PI * radius * radius;
    }

    public static double getRadius() {
        return radius;
    }

    public static double getDiameter() {
        return diameter;
    }

    public static double getArea() {
        return area;
    }

    public static void main(String[] args) {
        Circle circle = new Circle();
        System.out.println("Radius: " + circle.getRadius());
        System.out.println("Diameter: " + circle.getDiameter());
        System.out.println("Area: " + circle.getArea());

        circle.setRadius(5);
        System.out.println("Updated Radius: " + circle.getRadius());
        System.out.println("Updated Diameter: " + circle.getDiameter());
        System.out.println("Updated Area: " + circle.getArea());
    }
}
