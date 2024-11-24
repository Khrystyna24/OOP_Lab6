using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class TeachersWorkload
    {
        private string SubjectName;
        private int Semester;
        public int StudentCount;
        public SemesterControlInfo SemesterControl;
        public TeachersWorkload()
        {
            SubjectName = "Програмування";
            Semester = 1;
            StudentCount = 10;
            SemesterControl = new SemesterControlInfo(); 
        }
        public TeachersWorkload(string subjectName, int semester, int studentCount, SemesterControlInfo semesterControl)
        {
            SubjectName = subjectName;
            Semester = semester;
            StudentCount = studentCount;
            SemesterControl = semesterControl;
        }
        private string subjectName 
        { 
            get 
            {
                return SubjectName;
            }
            set
            {
                SubjectName = value; 
            }
        }
        private int semester 
        {
            get
            {
                return Semester;
            }
            set
            {
                Semester = value;
            }
        }
        private int studentCount
        {
            get
            {
                return StudentCount;
            }
            set
            {
                StudentCount = value;
            }
        }
        private SemesterControlInfo semesterControl
        {
            get
            {
                return SemesterControl;
            }
            set
            {
                SemesterControl = value;
            }
        }
        public TeachersWorkload(TeachersWorkload other)
        {
            SubjectName = other.SubjectName;
            Semester = other.Semester;
            StudentCount = other.StudentCount;
            SemesterControl = new SemesterControlInfo(other.SemesterControl);
        }
        public override string ToString()
        {
            return $"Назва предмета: {SubjectName}, Семестр: {Semester}, Кількість студентів: {StudentCount}, Підсумковий контроль {SemesterControl}";
        }
    }

    class SemesterControlInfo
    {
        private string ControlForm;
        private string Scale;
        private DateTime ExaminationDate;
        private DateTime ResultsFillingDate;
        private int Hours;
        public SemesterControlInfo()
        {
            ControlForm = "Екзамен";
            Scale = "5-бальна";
            ExaminationDate = DateTime.Now;
            ResultsFillingDate = DateTime.Now;
            Hours = 2;
        }
        public SemesterControlInfo(string controlForm, string scale, DateTime examinationDate, DateTime resultsFillingDate, int hours) 
        {
            ControlForm = controlForm;
            Scale = scale;
            ExaminationDate = examinationDate;
            ResultsFillingDate = resultsFillingDate;
            Hours = hours;
        }
        private string controlForm
        {
            get 
            { 
                return ControlForm;
            }
            set 
            { 
                ControlForm = value; 
            }
        }
        private string scale
        {
            get
            {
                return Scale;
            }
            set
            {
                Scale = value;
            }
        }
        private DateTime examinationDate
        {
            get
            {
                return ExaminationDate;
            }
            set
            {
                ExaminationDate = value;
            }
        }
        private DateTime resultsFillingDate
        {
            get
            {
                return ResultsFillingDate;
            }
            set
            {
                ResultsFillingDate = value;
            }
        }
        private int hours
        {
            get 
            { 
                return Hours;
            }
            set 
            { 
                Hours = value; 
            }
        }
        public int GetTotalTime(int studentCount)
        {
            return studentCount * Hours;
        }

        public bool IsHappeningToday() 
        {
           return ExaminationDate == ResultsFillingDate;
        }
        public SemesterControlInfo(SemesterControlInfo other)
        {
            ControlForm = other.ControlForm;
            Scale = other.Scale;
            ExaminationDate = other.ExaminationDate;
            ResultsFillingDate = other.ResultsFillingDate;
            Hours = other.Hours;
        }
        public override string ToString()
        {
            return $"Вид контролю: {ControlForm}, Шкала оцінювання: {Scale}, Дата проведення іспиту:{ExaminationDate}, Дата заповнення відомості: {ResultsFillingDate}, Кількість годин: {Hours}";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var defaultWorkload = new TeachersWorkload();
            var defaultSemesterControl = new SemesterControlInfo();

            Console.WriteLine("Інформація про предмет за замовчуванням:");
            DisplayWorkload(defaultWorkload);

            Console.WriteLine("Введіть кількість предметів:");
            int n = int.Parse(Console.ReadLine());

            var workloads = CreateWorkloads(n);


            while (true)
            {
                Console.WriteLine("\nМеню:\n1. Показати інформацію про проведення всіх іспитів \n2. Показати інформацію про проведення певного іспиту\n3. Перевірити, чи іспит відбувається сьогодні \n4. Перевірити загальний час на іспит\n5. Вийти");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAllWorkloads(workloads);
                        break;
                    case "2":
                        Console.WriteLine("Введіть індекс іспиту (починаючи від 0) для відображення:");
                        int index = int.Parse(Console.ReadLine());
                        if (index >= 0 && index < workloads.Count)
                        {
                            DisplayWorkload(workloads[index]);
                        }
                        else
                        {
                            Console.WriteLine("Недійсний індекс.");
                        }
                        break;
                    case "3": 
                        Console.WriteLine("Введіть індекс іспиту для перевірки, чи він відбувається сьогодні:");
                        int todayIndex = int.Parse(Console.ReadLine());
                        if (todayIndex >= 0 && todayIndex < workloads.Count)
                        {
                            bool isToday = workloads[todayIndex].SemesterControl.IsHappeningToday();
                            Console.WriteLine(isToday ? "Іспит проводиться сьогодні." : "Іспит не проводиться сьогодні.");
                        }
                        else
                        {
                            Console.WriteLine("Недійсний індекс.");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Введіть індекс іспиту для перевірки загального часу:");
                        int timeIndex = int.Parse(Console.ReadLine());
                        if (timeIndex >= 0 && timeIndex < workloads.Count)
                        {
                            int totalTime = workloads[timeIndex].SemesterControl.GetTotalTime(workloads[timeIndex].StudentCount);
                            Console.WriteLine($"Загальний час на іспит: {totalTime} годин.");
                        }
                        else
                        {
                            Console.WriteLine("Недійсний індекс.");
                        }
                        break;
                    case "5": 
                        Console.WriteLine("Програма завершується...");
                        return; 
                    default:
                        Console.WriteLine("Неправильний вибір. Спробуйте ще раз.");
                        break;
                }
            }



        }

        static List<TeachersWorkload> CreateWorkloads(int n)
        {
            var workloads = new List<TeachersWorkload>();

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nВкажіть деталі проведення іспиту {i + 1}:");

                Console.Write("Назва дисципліни: ");
                string subjectName = Console.ReadLine();

                Console.Write("Семестр: ");
                int semester = int.Parse(Console.ReadLine());

                Console.Write("Кількість студентів: ");
                int studentCount = int.Parse(Console.ReadLine());

                Console.Write("Вид контролю: ");
                string controlForm = Console.ReadLine();

                Console.Write("Шкала оцінювання: ");
                string scale = Console.ReadLine();

                Console.Write("Дата проведення іспиту (yyyy-MM-dd): ");
                DateTime examinationDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Дата заповнення відомості (yyyy-MM-dd): ");
                DateTime resultsFillingDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Кількість годин: ");
                int hours = int.Parse(Console.ReadLine());

                var semesterControl = new SemesterControlInfo(controlForm, scale, examinationDate, resultsFillingDate, hours);
                workloads.Add(new TeachersWorkload(subjectName, semester, studentCount, semesterControl));
            }

            return workloads;
        }

        static void DisplayWorkload(TeachersWorkload workload)
        {
            Console.WriteLine(workload);
        }

        static void DisplayAllWorkloads(List<TeachersWorkload> workloads)
        {
            for (int i = 0; i < workloads.Count; i++)
            {
                Console.WriteLine($"Іспит {i + 1}:");
                DisplayWorkload(workloads[i]);
            }
            Console.ReadKey();
        }

    }
}

