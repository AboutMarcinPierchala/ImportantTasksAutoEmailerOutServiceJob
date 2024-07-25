using ImpListScheduler;
using System;
using System.Threading;

class Program
{
    private readonly EmailSender _emailSender;

    static void Main(string[] args)
    {
        //Console.WriteLine("Scheduler started");

        //// Interval setting
        //TimeSpan interval = TimeSpan.FromHours(7);

        //while (true)
        //{
        //    // Get the current time
        //    DateTime nextRunTime = DateTime.Now.Add(interval);

        //    Console.WriteLine($"Next job will be at: {nextRunTime}");

        //    // Perform the scheduled task
        //    PerformScheduledTask();

        //    // Sleep until the next run time
        //    TimeSpan sleepTime = nextRunTime - DateTime.Now;
        //    if (sleepTime.TotalMilliseconds > 0)
        //    {
        //        Thread.Sleep(sleepTime);
        //    }
        //}
        PerformScheduledTask();
    }

    public static void PerformScheduledTask()
    {
        using(var db = new ApplicationDbContext())
        {
            DateTime now = DateTime.Now;
            var items = new List<Item>();
            items = db.Items.ToList();
            var mailBodyCloseToDepr = "";
            var mailBodyDeprecated = "";
            foreach (Item item in items)
            {
                
                DateTime itemDateTime = new DateTime(item.EndDate.Year, item.EndDate.Month, item.EndDate.Day);
                TimeSpan calculateDate = itemDateTime - now;

                if (calculateDate.Days > 0 && calculateDate.Days <= 7)
                {
                    mailBodyCloseToDepr += $"Item is close to be deprecated: {item.Name},  End Date: {item.EndDate}\n\n";
                    //var emailSender = EmailSender.SendEmailAsync("macintecmarcin@gmail.com", "Item is close to be deprecated", $"Item name: {item.Name}\n\nEnd Date: {item.EndDate}");
                    Console.WriteLine($"Task {item.Name} będzie przeterminowany do dnia: {item.EndDate}");
                }
                if (calculateDate.Days <= 0)
                {
                    mailBodyDeprecated += $"Item: {item.Name} has a deprecated date,  End Date: {item.EndDate}\n\n";
                    //var emailSender = EmailSender.SendEmailAsync("macintecmarcin@gmail.com", "ITEM IS DEPRECATED", $"Item: {item.Name} has a deprecated date: {item.EndDate}");
                    Console.WriteLine($"Task {item.Name} został przeterminowany z dniem: {item.EndDate}");
                }                
            }
            var mailBody = "Deprecated Items: \n\n" + 
                (String.IsNullOrEmpty(mailBodyDeprecated) ? "No Deprecated Items\n\n" : mailBodyDeprecated) +
                "\n\nItems close to be deprecated:\n\n" +
                (String.IsNullOrEmpty(mailBodyCloseToDepr) ? "No items that will become obsolete\n\n" : mailBodyCloseToDepr);

            var emailSender = EmailSender.SendEmailAsync("macintecmarcin@gmail.com", "IMP_LIST_DAILY_SUMMARY", mailBody);
        }
        // Write a message to the console
        //Console.WriteLine($"Task executed at: {DateTime.Now}. Hello, this is your scheduled task!");
    }
}