public class warmup_sleepin {
    public boolean sleepIn(boolean weekday, boolean vacation) {
        return !weekday || vacation;
    }
    public static void main(String[] args) {
        warmup_sleepin sleeping = new warmup_sleepin();
        
        System.out.println("sleepIn(false, false) > " + sleeping.sleepIn(false, false));
        System.out.println("sleepIn(true, false) > " + sleeping.sleepIn(true, false));
        System.out.println("sleepIn(false, true) > " + sleeping.sleepIn(false, true));
    }
}
