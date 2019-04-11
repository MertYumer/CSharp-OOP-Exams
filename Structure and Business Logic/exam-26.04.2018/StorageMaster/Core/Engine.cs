namespace StorageMaster.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Engine
    {
        private StorageMaster storageMaster;

        public Engine()
        {
            this.storageMaster = new StorageMaster();
        }

        public void Run()
        {
            while (true)
            {
                var input = Console.ReadLine().Split();

                string command = input[0];

                try
                {
                    switch (command)
                    {
                        case "AddProduct":
                            Console.WriteLine(storageMaster
                                .AddProduct(input[1], double.Parse(input[2])));
                            break;

                        case "RegisterStorage":
                            Console.WriteLine(storageMaster
                                .RegisterStorage(input[1], input[2]));
                            break;

                        case "SelectVehicle":
                            Console.WriteLine(storageMaster
                                .SelectVehicle(input[1], int.Parse(input[2])));
                            break;

                        case "LoadVehicle":
                            Console.WriteLine(storageMaster
                                .LoadVehicle(input.Skip(1).ToList()));
                            break;

                        case "SendVehicleTo":
                            Console.WriteLine(storageMaster
                                .SendVehicleTo(input[1], int.Parse(input[2]), input[3]));
                            break;

                        case "UnloadVehicle":
                            Console.WriteLine(storageMaster
                                .UnloadVehicle(input[1], int.Parse(input[2])));
                            break;

                        case "GetStorageStatus":
                            Console.WriteLine(storageMaster
                                .GetStorageStatus(input[1]));
                            break;

                        case "END":
                            Console.WriteLine(this.storageMaster.GetSummary());
                            return;
                    }
                }

                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine($"Error: {ioe.Message}");
                }
            }
        }
    }
}
