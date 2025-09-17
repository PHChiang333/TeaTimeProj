using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeaTimeProj.Utility
{
    public static class SD
    {
        public const string Role_Customer = "Customer";
        public const string Role_Employee = "Employee";
        public const string Role_Manager = "Manager";
        public const string Role_Admin = "Admin";

        // Order Status
        //Pending -> Processing -> Ready -> Completed
        public const string StatusPending = "Pending";
        //public const string StatusApproved = "Approved";
        public const string StatusInProcess = "Processing";
        public const string StatusReady = "Ready";
        public const string StatusCompleted = "Completed";

        public const string StatusCanceled = "Canceled";
    }
}
