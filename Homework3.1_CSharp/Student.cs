namespace Homework3._1_CSharp
{
    public class Student
    {
        public int sex { get; set; }
        private double gpa;

        public Student(int sex, double gpa)
        {
            this.sex = sex;
            this.gpa = gpa;
        }

        public double getGpa() { return this.gpa; }
    }
}
