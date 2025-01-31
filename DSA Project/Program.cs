using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSA_Project
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class Vehicle
    {
        private string vehicleID;
        private string model;
        private int year;
        private float mileage;
        private string maintenanceSchedule;

        public Vehicle(string id, string mdl, int yr, float ml, string mnt)
        {
            vehicleID = id;
            model = mdl;
            year = yr;
            mileage = ml;
            maintenanceSchedule = mnt;
        }

        public string GetID() => vehicleID;
        public string GetModel() => model;
        public int GetYear() => year;
        public float GetMileage() => mileage;
        public string getMaintenanceSchedule() { return maintenanceSchedule; }
        public void UpdateMileage(float newMileage) => mileage = newMileage;
        public void UpdateMaintenanceSchedule(string newSchedule) => maintenanceSchedule = newSchedule;
    }
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    class UserNode
    {
        public User User { get; set; }
        public UserNode Next { get; set; }
    }
    public class UserList
    {
        private UserNode head;

        public UserList()
        {
            head = null;
        }
        public void AddUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both a username and a password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (IsUsernameTaken(username))
            {
                MessageBox.Show("Username already exists. Please choose a different username.", "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return;
            }
            var newUser = new UserNode { User = new User { Username = username, Password = password } };
            newUser.Next = head;
            head = newUser;
            MessageBox.Show("User registered successfully!", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool IsUsernameTaken(string username)
        {
            UserNode temp = head;
            while (temp != null)
            {
                if (temp.User.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
                {
                    return true; 
                }
                temp = temp.Next;
            }
            return false; 
        }

        public void RemoveUser(string username,string password)
        {
            UserNode prev = null;
            UserNode current = head;

            while (current != null)
            {
                if (current.User.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && current.User.Password.Equals(password, StringComparison.OrdinalIgnoreCase))
                {
                    
                    if (prev == null)
                        head = current.Next;
                    else
                        prev.Next = current.Next;
                    MessageBox.Show("User removed successfully!!", "Removal Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                prev = current;
                current = current.Next;
            }
            MessageBox.Show("User not found.", "User not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public bool ValidateUser(string username, string password)
        {
            UserNode temp = head;
            while (temp != null)
            {
                if (temp.User.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
                {
                    return temp.User.Password == password;
                }
                temp = temp.Next;
            }
            return false; 
        }
    }
    public class Node
    {
        public Vehicle vehicle { get; set; }
        public Node Next { get; set; }
    }
    public class Fleet
    {
        private Node head;
        public Node GetHead()
        {
            return head;
        }
        public Fleet()
        {
            head = null;
        }

        public void addVehicle(Vehicle v)
        {
            Node newNode = new Node { vehicle = v, Next = null };
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                Node temp = head;
                while (temp.Next != null)
                {
                    temp = temp.Next;
                }
                temp.Next = newNode;
            }
        }
        public void removeVehicle(string id)
        {
            Node temp = head;
            Node prev = null;

            while (temp != null && temp.vehicle.GetID() != id)
            {
                prev = temp;
                temp = temp.Next;
            }

            if (temp == null)
            {
                MessageBox.Show($"Vehicle with ID {id} not found.", "Invalid", MessageBoxButtons.OK);
                return;
            }

            if (prev == null)
            {
                head = temp.Next;
            }
            else
            {
                prev.Next = temp.Next;
            }
            MessageBox.Show($"Vehicle with ID {id} removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public Vehicle searchVehicle(string id)
        {
            Node temp = head;
            while (temp != null)
            {
                if (temp.vehicle.GetID() == id)
                {
                    return temp.vehicle;
                }
                temp = temp.Next;
            }
            return null;
        }
        public void updateMileage(string id, float newMileage)
        {
            Node temp = head;
            while (temp != null)
            {
                if (temp.vehicle.GetID() == id)
                {
                    temp.vehicle.UpdateMileage(newMileage);
                    MessageBox.Show($"Mileage updated successfully for vehicle with ID {id}.", "Success", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                temp = temp.Next;
            }
            MessageBox.Show($"Vehicle with ID {id} not found.", "Invalid", MessageBoxButtons.OK);
        }
        public void updateMaintenanceSchedule(string id, string newSchedule)
        {
            Node temp = head;
            while (temp != null)
            {
                if (temp.vehicle.GetID() == id)
                {
                    temp.vehicle.UpdateMaintenanceSchedule(newSchedule);
                    MessageBox.Show($"Maintenance schedule updated successfully for vehicle with ID {id}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                temp = temp.Next;
            }
            MessageBox.Show($"Vehicle with ID {id} not found.", "Invalid", MessageBoxButtons.OK);
        }
        public List<Vehicle> GetAllVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            Node temp = head;
            while (temp != null)
            {
                vehicles.Add(temp.vehicle);
                temp = temp.Next;
            }
            return vehicles;
        }

    }
}

