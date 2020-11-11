using System;

namespace pragueparking
{
    class MainClass
    {

        int sizeofparking = 100;
        static string[] parking = new string[100];


        static void Main()
        {



            for (int i = 0; i < parking.Length; i++) { parking[i] = null; }




            Console.WriteLine("welcome to prague parking!!");
            Console.WriteLine("1: Park");
            Console.WriteLine("2: Move vehicle to another parking spot");
            Console.WriteLine("3: Find your vehicle");
            Console.WriteLine("4: Exit garage");

            string menu = Console.ReadLine();

            if (menu == "1")
            {
                park();

            }

            else if (menu == "2")
            {
                movevehicle();
                ReturnToMenu();
            }

            else if (menu == "3")
            {
                Console.WriteLine("Kindly enter your registration number:");
                string regnum = Console.ReadLine();
                string values = findvehicle(regnum);
                string[] valuess = values.Split(',');
                if (int.Parse(valuess[0]) != 404) { Console.WriteLine("U can find your vehicule in the slot:{0}", int.Parse(valuess[0]) + 1); }

                ReturnToMenu();
            }

            else if (menu == "4")

            {
                string regnum = "";
                Console.WriteLine("enter you registration number :");
                regnum = Console.ReadLine();
                exitgarage(regnum);
                ReturnToMenu();

            }
        }

        public static void park()
        {

            string regnumber = "";
            Console.WriteLine("please enter the type of the vehicle here,");
            Console.Write("if its a car then type 1 , if its a motorcycle then type 2:");
            int cartype = int.Parse(Console.ReadLine());


            if (cartype == 1)

            {
                Console.WriteLine("type in the reg number: ");
                do
                {
                    regnumber = Console.ReadLine();
                    if (regnumber.Length > 10) { Console.WriteLine("please enter a regnumber lesser than 10 characteres"); }
                } while (regnumber.Length > 10);
                parkspaceV(parking, regnumber);

                ReturnToMenu();


            }
            else if (cartype == 2)
            {
                Console.WriteLine("type in the reg number: ");
                do
                {
                    regnumber = Console.ReadLine();
                    if (regnumber.Length > 10) { Console.WriteLine("please enter a regnumber lesser than 10 characteres"); }
                } while (regnumber.Length > 10);
                parkspaceM(parking, regnumber);

                ReturnToMenu();
            }

        }

        public static void movevehicle()
        {
            String regnum = "";
            bool found = false;
            int type = 0, i = 0;

            Console.Clear();
            Console.WriteLine("please enter your registration number:");
            regnum = Console.ReadLine();
            Console.WriteLine("where do you want to move the vehicle to: ");
            int slot = int.Parse(Console.ReadLine());
            string find = findvehicle(regnum);
            string[] findl = find.Split(','); i = int.Parse(findl[0]); type = int.Parse(findl[1]);
            if ((i == parking.Length - 1) && (found == false)) { Console.WriteLine("We can't find your vehicule "); }
            else
            {
                if (parking[slot - 1] == null)
                {
                    if (type == 1)
                    {
                        parking[i] = null;
                        parking[slot - 1] = regnum + ",V";
                        Console.WriteLine("Car moved to place:{0}", slot);

                    }
                    else removemoto(regnum, i);

                }
                else
                {
                    if (type == 1)
                    {
                        Console.WriteLine("slot occupied, please choose another slot");
                    }
                    else
                    {

                        string[] test = parking[slot - 1].Split(',');
                        if (test.Length == 4) Console.WriteLine("Slot occupied, please choose another slot");
                        else
                        {
                            removemoto(regnum, i);
                            parking[slot - 1] = parking[slot - 1] + "," + regnum + ",M";
                            Console.WriteLine("Moto Moved to place: {0}", slot);
                        }
                    }
                }
            }

        }

        public static void removemoto(string regnum, int index)
        {
            string[] test = parking[index].Split(',');
            if (test.Length == 2) { parking[index] = null; }
            else
            {
                if (regnum == test[0]) parking[index] = test[2] + ",M";
                else parking[index] = test[0] + ",M";
            }
        }

        public static string findvehicle(string regnum)
        {
            int i = 0;
            bool found = false;
            int type = 0;

            while (!found && (i < parking.Length))
            {
                if (parking[i] != null)
                {
                    string[] test = parking[i].Split(',');
                    if (test.Length == 2)
                    {
                        if (regnum == test[0])
                        {
                            found = true;
                            if (test[1] == "V") type = 1;
                            else type = 2;
                        }
                        else i++;
                    }
                    else
                    {
                        if ((regnum == test[0]) || (regnum == test[2])) { found = true; type = 2; }
                        else i++;
                    }
                }
                else i++;
            }
            if ((i == parking.Length - 1) || (found == false)) { Console.WriteLine("We can't find your vehicule "); return "404" + ",0"; }

            else
            {

                return i + "," + type;

            }
        }


        public static void parkspaceV(string[] t, string regnum)
        {
            int i = 0;
            bool foundspace = false;
            while ((foundspace == false) && (i < t.Length - 1))
            {
                if (t[i] == null) { foundspace = true; }
                else i++;
            }
            if ((i == t.Length - 1) && (foundspace == false)) { Console.WriteLine("No spaces were found! "); }
            else
            {
                Console.WriteLine(" Car {0} please proceed to place {1}", regnum, i + 1);
                t[i] = regnum + "," + "V";
            }

        }

        public static void parkspaceM(string[] t, string regnum)
        {
            int i = 0;
            int count = 0;
            bool foundspace = false;
            while ((foundspace == false) && (i < t.Length))
            {
                if (t[i] == null)

                {
                    foundspace = true;
                    count = 1;
                }
                else
                {


                    string[] test = t[i].Split(','); //to check whats in the parking slot

                    if (test.Length == 2)
                    {
                        if (test[1] == "V") i++;        //efter första , finns det V
                        else
                        {
                            count = 2;
                            foundspace = true;
                        }

                    }
                    else i++;

                }
            }
            if ((i == t.Length - 1) && (foundspace == false)) { Console.WriteLine("No spaces were found! "); }
            else
            {
                Console.WriteLine(" Motorcycle {0} please proceed to place {1}", regnum, i + 1);
                if (count == 1) t[i] = regnum + "," + "M";
                else t[i] = t[i] + "," + regnum + "," + "M";

            }

        }


        public static void exitgarage(string regnum)
        {
            string A = findvehicle(regnum);
            string[] values = A.Split(',');
            int i = int.Parse(values[0]);
            if (i != 404)
            {
                if (int.Parse(values[1]) == 1) parking[i] = null;
                else removemoto(regnum, i);
            }

        }

        public static void ReturnToMenu()
        {

            Console.WriteLine("****************************************************************");
            Console.WriteLine("welcome to prague parking!!");
            Console.WriteLine("1: Park");
            Console.WriteLine("2: Move vehicle to another parking spot");
            Console.WriteLine("3: Find your vehicle");
            Console.WriteLine("4: Exit garage");

            string menu = Console.ReadLine();

            if (menu == "1")
            {
                park();

            }



            else if (menu == "2")
            {
                movevehicle();
                ReturnToMenu();
            }



            else if (menu == "3")
            {

                Console.WriteLine("Kindly enter your registration number:");
                string regnum = Console.ReadLine();
                string values = findvehicle(regnum);
                string[] valuess = values.Split(',');
                if (int.Parse(valuess[0]) != 404) { Console.WriteLine("U can find your vehicule in the slot:{0}", int.Parse(valuess[0]) + 1); }
                ReturnToMenu();
            }

            else if (menu == "4")

            {
                string regnum = "";
                Console.WriteLine("enter you registration number :");
                regnum = Console.ReadLine();
                exitgarage(regnum);
                ReturnToMenu();
            }

        }

    }
}







