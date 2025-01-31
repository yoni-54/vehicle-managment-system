using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DSA_Project
{
    public partial class Form2 : Form
    {
        Fleet fleet = new Fleet();

        private UserList userList;
        public Form2(UserList userList)
        {
            this.userList = userList;
            InitializeComponent();
            LoadFleetData();
            PopulateDataGridView();
        }
        private void label11_Click(object sender, EventArgs e)
        {
            Application.Exit();
            SaveFleetData();
        }
        private void SaveFleetData()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("fleetdata.txt"))
                {
                    foreach (Vehicle vehicle in fleet.GetAllVehicles())
                    {
                        writer.WriteLine($"{vehicle.GetID()},{vehicle.GetModel()},{vehicle.GetYear()},{vehicle.GetMileage()},{vehicle.getMaintenanceSchedule()}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving fleet data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string vehicleID = textBox3.Text;
            string model = textBox4.Text;
            string yearString = textBox2.Text;
            string mileageString = textBox5.Text;
            string maintenanceSchedule = textBox1.Text;

            if (string.IsNullOrWhiteSpace(vehicleID) || string.IsNullOrWhiteSpace(model) || string.IsNullOrWhiteSpace(yearString) || string.IsNullOrWhiteSpace(mileageString) || string.IsNullOrWhiteSpace(maintenanceSchedule))
            {
                MessageBox.Show("Enter all fields", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                                   
            else
            {
                int year = int.Parse(textBox2.Text);
                float mileage = float.Parse(textBox5.Text);
                if (IsVehicleIdUnique(vehicleID))
                {
                    Vehicle vehiclePtr = new Vehicle(vehicleID, model, year, mileage, maintenanceSchedule);
                    fleet.addVehicle(vehiclePtr);
                    MessageBox.Show($"Vehicle with ID {vehicleID} added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    PopulateDataGridView();
                }
                else
                {
                    MessageBox.Show("Vehicle ID must be unique.", "Duplicate ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }
        private void LoadFleetData()
        {
            try
            {
                if (File.Exists("fleetdata.txt"))
                {
                    using (StreamReader reader = new StreamReader("fleetdata.txt"))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');
                            if (parts.Length == 5)
                            {
                                string vehicleID = parts[0];
                                string model = parts[1];
                                int year = int.Parse(parts[2]);
                                float mileage = float.Parse(parts[3]);
                                string maintenanceSchedule = parts[4];
                                Vehicle vehicle = new Vehicle(vehicleID, model, year, mileage, maintenanceSchedule);
                                fleet.addVehicle(vehicle);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading fleet data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool IsVehicleIdUnique(string vehicleId)
        {
            Node temp = fleet.GetHead();
            while (temp != null)
            {
                if (temp.vehicle.GetID() == vehicleId)
                {
                    return false;
                }
                temp = temp.Next;
            }
            return true; 
        }
        private void PopulateDataGridView()
        {
            dataGridView1.Rows.Clear();
            Node temp = fleet.GetHead();
            while (temp != null)
            {
                Vehicle vehicle = temp.vehicle;
                dataGridView1.Rows.Add(vehicle.GetID(), vehicle.GetModel(), vehicle.GetMileage(), vehicle.GetYear(), vehicle.getMaintenanceSchedule());
                temp = temp.Next;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string vehicleID = textBox3.Text;
            fleet.removeVehicle(vehicleID);
            PopulateDataGridView();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string updateVehicleMileageID = textBox3.Text;
            float updateVehicleMileage = float.Parse(textBox5.Text);
            fleet.updateMileage(updateVehicleMileageID, updateVehicleMileage);
            PopulateDataGridView();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string updateVehicleMaintenanceScheduleID = textBox3.Text;
            string updateVehicleMaintenanceSchedule = textBox1.Text;
            fleet.updateMaintenanceSchedule(updateVehicleMaintenanceScheduleID, updateVehicleMaintenanceSchedule);
            PopulateDataGridView();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            PopulateDataGridView();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            string searchId = textBox6.Text;
            Vehicle foundVehicle = fleet.searchVehicle(searchId);
            if (foundVehicle != null)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Rows.Add(
                foundVehicle.GetID(),
                foundVehicle.GetModel(),
                foundVehicle.GetYear(),
                foundVehicle.GetMileage(),
                foundVehicle.getMaintenanceSchedule());
                
            }
            else
            {
                MessageBox.Show("Vehicle not found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PopulateDataGridView();
        }
        private void label14_Click(object sender, EventArgs e)
        {
            Login login = new Login(userList);
            login.Show();
            this.Hide();
            SaveFleetData();
        }
        private void label15_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            textBox4.Clear();
            textBox2.Clear();
            textBox5.Clear();
            textBox1.Clear();
        }
    }
}
