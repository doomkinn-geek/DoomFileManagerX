using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tree_List_View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Department> deptlist = new List<Department>();

        private List<Student> students = new List<Student>();


        public MainWindow()

        {

            InitializeComponent();



            List<Subject> sublist1 = new List<Subject>();

            sublist1.Add(new Subject(100101, "Pre Calculus"));

            sublist1.Add(new Subject(100210, "Calculus 1"));
            sublist1.Add(new Subject(100211, "Calculus 2"));
            sublist1.Add(new Subject(100212, "Calculus 3"));
            sublist1.Add(new Subject(100218, "Linear Algebra"));
            sublist1.Add(new Subject(100213, "Differential Equation"));
            Department dept1 = new Department();
            dept1.Name = "Math";
            dept1.Chairman = "Bob Smit";
            dept1.Description = "Applied Math Department";
            dept1.Subjects = sublist1;


            deptlist.Add(dept1);

            List<Subject> sublist2 = new List<Subject>();
            sublist2.Add(new Subject(200100, "Business Accounting"));
            sublist2.Add(new Subject(200101, "Accounting 1"));
            sublist2.Add(new Subject(200102, "Accounting 2"));
            sublist2.Add(new Subject(200201, "Accounting 3"));
            sublist2.Add(new Subject(200201, "Accounting 4"));
            sublist2.Add(new Subject(200203, "Cost Accounting"));
            sublist2.Add(new Subject(200205, "Tax Accounting"));
            sublist2.Add(new Subject(200214, "Auditing"));
            Department dept2 = new Department();

            dept2.Name = "Accounting";
            dept2.Chairman = "Jon Thompson";
            dept2.Description = "Accounting and Management";
            dept2.Subjects = sublist2;

            deptlist.Add(dept2);

            List<Subject> sublist3 = new List<Subject>();
            sublist3.Add(new Subject(300101, "Introduction to CS"));
            sublist3.Add(new Subject(300106, "OOP"));
            sublist3.Add(new Subject(300121, "Visual Basic"));
            sublist3.Add(new Subject(300170, "Security 1"));
            sublist3.Add(new Subject(300201, "Computer Science"));
            sublist3.Add(new Subject(300208, "C++ Programming"));
            sublist3.Add(new Subject(300232, "DB Administrator"));
            sublist3.Add(new Subject(300241, "Networking"));
            Department dept3 = new Department();
            dept3.Name = "Computer Science";
            dept3.Chairman = "Chris Nathan";
            dept3.Description = "Computer Engineering and Science";
            dept3.Subjects = sublist3;

            deptlist.Add(dept3);

            students.Add(new Student(10, "Alex", "Martin", 100210, "A"));
            students.Add(new Student(10, "Alex", "Martin", 300101, "A"));
            students.Add(new Student(10, "Alex", "Martin", 200101, "B"));
            students.Add(new Student(12, "Joe", "Brown", 100101, "A"));
            students.Add(new Student(12, "Joe", "Brown", 300170, "B"));
            students.Add(new Student(12, "Joe", "Brown", 200203, "A"));
            students.Add(new Student(16, "David", "Frank", 100212, "A"));
            students.Add(new Student(16, "David", "Frank", 100218, "A"));
            students.Add(new Student(16, "David", "Frank", 100213, "A"));
            students.Add(new Student(16, "David", "Frank", 300101, "A"));
            students.Add(new Student(16, "David", "Frank", 200100, "B"));
            students.Add(new Student(18, "Patrick", "Justin", 200100, "A"));
            students.Add(new Student(18, "Patrick", "Justin", 300101, "A"));
            students.Add(new Student(18, "Patrick", "Justin", 100210, "A"));
            students.Add(new Student(18, "Patrick", "Justin", 100211, "A"));
            students.Add(new Student(25, "Ken", "White", 100210, "B"));
            students.Add(new Student(25, "Ken", "White", 300101, "B"));
            students.Add(new Student(25, "Ken", "White", 200101, "A"));

            DataContext = deptlist;

        }

        private void tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            list.Items.Clear();

            Subject selectedSubject = tree.SelectedItem as Subject;

            if (selectedSubject != null)
            {
                int subjectID = selectedSubject.ID;

                IEnumerable<Student> selectedStudents = from fn in students
                                                        where fn.SubjectID == subjectID
                                                        select fn;

                foreach (Student student in selectedStudents)
                {
                    list.Items.Add(student);
                }
            }

            Department dept = tree.SelectedItem as Department;

            if (dept != null)
            {
                list.Items.Add(dept);
            }
        }
    }

    public class MyTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DepartmentTemplate
        { get; set; }

        public DataTemplate SubjectTemplate
        { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Department dept = item as Department;

            if (dept != null)
                return DepartmentTemplate;

            Student student = item as Student;

            if (student != null)
                return SubjectTemplate;

            return base.SelectTemplate(item, container);
        }
    }

    public class SubjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
        object parameter, CultureInfo culture)
        {

            TreeView selectedItem = value as TreeView;
            Subject subject = selectedItem.SelectedItem as Subject;
            return subject.Name;
        }        

        public object ConvertBack(object value, Type targetType,
        object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }        
    }

    public class Subject
    {
        public Subject(int id, String name)
        {
            ID = id;
            Name = name;
        }

        public int ID
        { get; set; }

        public String Name
        { get; set; }
    }

    public class Department
    {
        public Department()
        {
            this.Subjects = new List<Subject>();
        }

        public String Name
        { get; set; }

        public String Chairman
        { get; set; }

        public String Description
        { get; set; }

        public List<Subject> Subjects
        { get; set; }
    }

    public class Student
    {
        public Student()
        {
        }

        public Student(int id, String firstName, String lastName, int subjectID, String grade)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            SubjectID = subjectID;
            Grade = grade;
        }

        public int ID
        { get; set; }

        public String FirstName
        { get; set; }

        public String LastName
        { get; set; }

        public int SubjectID
        { get; set; }

        public String Grade
        { get; set; }
    }
}

