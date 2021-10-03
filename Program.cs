using System;
using System.Collections.Generic;

using System.IO;
using System.Text;
namespace proj1
{
    class TeacherModel
    {

        public int id { get; set; }
        public string Name { get; set; }

        public string Class { get; set; }

        public string Section { get; set; }

    }
    class TeacherBO
    {
        public List<TeacherModel> teachers { get; set; }

        public TeacherBO()
        {
            teachers = new List<TeacherModel>();
        }

        public List<TeacherModel> GetAllTeachers()
        {
            string fpath = "/Users/maha/Projects/proj1/proj1/teacher.txt";
            List<TeacherModel> teachers = new List<TeacherModel>();
            foreach (string line in File.ReadLines(fpath))
            {
                TeacherModel t = CreateModel(line);
                teachers.Add(t);
            }
            return teachers;
        }


        public TeacherModel CreateModel(string str)
        {


            string[] st = str.Split(" ");

            int id = Convert.ToInt32(st[0]);
            string Name = st[1];
            string Class = st[2];
            string Section = st[3];

            return new TeacherModel { id = id, Name = Name, Class = Class, Section = Section };
        }

        public void DisplayAllData()
        {
            teachers = GetAllTeachers();
            foreach (TeacherModel t in teachers)
            {
                Console.WriteLine($"{t.id} {t.Name} {t.Class} {t.Section}");
            }
        }


        public void Update(TeacherModel t, int id)
        {


            teachers = GetAllTeachers();
            int index = teachers.FindIndex(x => x.id == id);

            teachers[index] = t;

            WriteToFile(teachers);
        }



        public void Append(TeacherModel t)
        {
            string fpath = "/Users/maha/Projects/proj1/proj1/teacher.txt";
            StreamWriter fname = File.AppendText(fpath);

            fname.WriteLine($"{t.id} {t.Name} {t.Class} {t.Section}");
            fname.Close();
        }

        public void Add(TeacherModel t)
        {
            string fpath = "/Users/maha/Projects/proj1/proj1/teacher.txt";
            StreamWriter fname = new StreamWriter(fpath, false);

            fname.WriteLine($"{t.id} {t.Name} {t.Class} {t.Section}");
            fname.Close();
        }

        public bool CheckId(int id)
        {
            teachers = GetAllTeachers();
            int index = teachers.FindIndex(x => x.id == id);

            if (index == -1)
                return false;
            return true;
        }

        public void WriteToFile(List<TeacherModel> teachers)
        {

            int i = 0;
            foreach (TeacherModel t in teachers)
            {
                if (i == 0)
                    Add(t);
                else
                    Append(t);
                i++;
            }

        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            TeacherBO context = new TeacherBO();

            TeacherModel teacher;


            while (true)
            {
                Console.Write(" 1. Display Teachers \n 2. Add Teacher \n 3. Update Teacher Information \n  4. Exit \n");
                Console.Write("Enter your input : ");


                try
                {
                    int newInfo = Convert.ToInt32(Console.ReadLine());



                    if (newInfo == 4)
                    {
                        Console.WriteLine($"User given input is {newInfo} ");
                        break;
                    }
                    switch (newInfo)
                    {


                        case 1:

                            context.DisplayAllData();
                            Console.WriteLine("\n");
                            break;

                        case 2:

                          
                            Console.Write("Enter id number : ");
                            try
                            {
                                int id = Convert.ToInt32(Console.ReadLine());
                                if (context.CheckId(id))
                                {
                                    Console.WriteLine($"this id :{id} already exists in the database");
                                }
                                else
                                {
                                    Console.Write("Enter Teacher name : ");
                                    string Name = Console.ReadLine();
                                    Console.Write("Enter Class the teacher teaches : ");
                                    string Class = Console.ReadLine();
                                    Console.Write("Enter Section the teacher takes : ");
                                    string Section = Console.ReadLine();

                                    teacher = new TeacherModel { id = id, Name = Name, Class = Class, Section = Section };
                                    context.Append(teacher);

                                    Console.WriteLine(" data is added to the Teahers DataBase");
                                }

                                Console.WriteLine("\n");

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error :" + ex.Message);
                            }
                            break;


                        case 3:

                            Console.WriteLine("Existing Data : ");
                            context.DisplayAllData();
                            Console.WriteLine("\n");
                            Console.Write("Enter id number to be updated : ");

                            try
                            {
                                int update_id = Convert.ToInt32(Console.ReadLine());

                                if (context.CheckId(update_id))
                                {
                                    Console.Write("Enter Teacher name : ");
                                    string Name = Console.ReadLine();
                                    Console.Write("Enter Class  : ");
                                    string Class = Console.ReadLine();
                                    Console.Write("Enter Section : ");
                                    string Section = Console.ReadLine();

                                    teacher = new TeacherModel { id = update_id, Name = Name, Class = Class, Section = Section };
                                    context.Update(teacher, update_id);

                                    Console.WriteLine($"Database is updated successfully");
                                    Console.WriteLine("\n Updated Teachers Database is : \n");
                                    context.DisplayAllData();

                                }
                                else
                                {
                                    Console.WriteLine($"There is no entry avaialable with the given id :{update_id}");
                                }
                                Console.WriteLine("\n");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error : " + ex.Message);
                            }

                            break;

                        default:
                            Console.WriteLine($"{newInfo} is invalid input from the user");
                            Console.WriteLine("\n");
                            break;
                    }
                }

                catch (FormatException)
                {
                    Console.WriteLine("Incorrect input , please enter input in a range 1-5");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error : " + ex.Message);
                }


            }



        }
    }
}