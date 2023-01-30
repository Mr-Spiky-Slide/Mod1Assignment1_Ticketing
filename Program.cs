string file = "tickets.csv";
string choice;
bool addTicket = false;

StreamWriter sw0 = new StreamWriter(file);
sw0.WriteLine("TicketID, Summary, Status, Priority, Assigned, Submitter, Watching");
sw0.Close();



do
{
    // ask user a question
    Console.WriteLine("1) Read data from file.");
    Console.WriteLine("2) Create file from data.");
    Console.WriteLine("Enter any other key to exit.");
    // input response
    choice = Console.ReadLine();

    if (choice == "1")
    {
        // read data from file
        if (File.Exists(file))
        {
            // read data from file
            StreamReader sr = new StreamReader(file);
            while (!sr.EndOfStream)
            {
                string watchersStr = null;
                string line = sr.ReadLine();
                // convert string to array, splitting it at the comma "," 
                string[] arr = line.Split(',');
                //Organize the watchers
                string[] watchers = arr[6].Split('|');

                foreach (string name in watchers)
                {
                    watchersStr += name;
                }
                //display array data
                Console.WriteLine($"TicketID: {arr[0]}, Summary: {arr[1]}, Status: {arr[2]}, Priority: {arr[3]}, Submitter: {arr[4]}, Assigned: {arr[5]}, Watching: {watchersStr}");
                
            }
            sr.Close();
            
        }
        else
        {
            Console.WriteLine("File does not exist");
        }
    }
    else if (choice == "2")
    {
        do{
        // create file from data
        StreamWriter sw1 = new StreamWriter(file, append: true);

        string watchers = null;
        Ticket t = new Ticket();
        int amount = t.Watching.Count();
        for (int j = 0; j < amount; j++)
        {
            if (j + 1 == amount)
            {
                watchers += t.Watching[j];
            }
            else
            {
                watchers += $"{t.Watching[j]}|";
            }
        }
        sw1.WriteLine($"{t.TicketID},{t.Summary},{t.Status},{t.Priority},{t.Assigned},{t.Submitter},{watchers}");

        sw1.Close();

        Console.WriteLine("Add another ticket? (Y/N)");
        string resp = Console.ReadLine().ToUpper();
        if (resp == "Y") { addTicket = true; }
        else if(resp == "N") { addTicket = false; }
        }while(addTicket);
    }
} while (choice == "1" || choice == "2");

class Ticket
{
    //Properties
    public int TicketID { get; set; }
    public string Summary { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public string Assigned { get; set; }
    public string Submitter { get; set; }
    public List<string> Watching = new List<String>();

    public Ticket()
    {
        Console.WriteLine("Enter a unique TicketID: ");
        TicketID = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter ticket summary: ");
        Summary = Console.ReadLine();

        Console.WriteLine("Enter ticket status (Open, Closed): ");
        Status = Console.ReadLine();

        Console.WriteLine("Enter ticket priority (High, Medium, Low): ");
        Priority = Console.ReadLine();

        Console.WriteLine("Enter ticket Assignee: ");
        Assigned = Console.ReadLine();

        Console.WriteLine("Enter ticket Submitter: ");
        Submitter = Console.ReadLine();

        bool loopList = true;
        while (loopList == true)
        {
            Console.WriteLine("Enter watcher name or ~ to stop: ");
            string inputWatch = Console.ReadLine();
            if (inputWatch == "~")
            {
                loopList = false;
            }
            else
            {
                Watching.Add(inputWatch);
            }
        }

    }

}

