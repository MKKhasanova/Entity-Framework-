using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace автовокзал
{
    public partial class Form1 : Form
    {
        avtovokEntities db = new avtovokEntities();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // UpdateDataGridViewBefore();
            //UpdateDataGridView1();
        }
        private void UpdateDataGridViewBefore()//обновление dataGridView1
        {
            var routes_ = from routes in db.Rout
                          join town in db.Town on routes.Town_ID equals town.Town_ID
                          select new
                          {
                              Routes_ID = routes.Rout_ID,
                              Routes_Number = routes.Routes_number,
                              Departure = routes.Otk,
                              Departure_Time = routes.Vrem_otp,
                              Destination = town.Town1,
                              Destination_Time = routes.Vrem_Prib,
                              Koll_Biletov = routes.Kol,
                              Info = routes.Info
                          };
            dataGridView3.DataSource = routes_.ToList();
        }
        private void UpdateDataGridView1()//обновление dataGridView2
        {
            var stopover = from stopoveR in db.Stopover
                           join village in db.Village on stopoveR.Village_ID equals village.Village_ID
                           join rout in db.Rout on stopoveR.Routes_ID equals rout.Rout_ID
                           select new
                           {
                               Routes_Namber = rout.Routes_number,
                               Data = textBox2.Text,
                               Departure = "Набережные Челны",
                               Vremya_otp_iz_NCH = rout.Vrem_Prib_NCH,
                               Village = village.Village_Name,
                               Vremiya_Prib = rout.Vrem_Prib,
                               Price = stopoveR.Price
                           };
            dataGridView2.DataSource = stopover.ToList();
        }

       /* private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Убедимся, что была кликнута не заголовочная ячейка
            {
                int routesID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Routes_Number"].Value); // Получаем номер маршрута из выбранной строки

                var selectedRoutes = db.Rout.FirstOrDefault(m => m.Routes_number == routesID); // Находим соответствующую запись в базе данных

                if (selectedRoutes != null)
                {
                    string inf = selectedRoutes.Info; // Получаем информацию о маршруте
                    string[] infoParts = inf.Split(' '); // Разбиваем информацию на части

                    string labelText = ""; // Подготовим текст для label
                    for (int i = 0; i < infoParts.Length - 1; i += 2) // Теперь используем шаг 2, так как у нас две части информации (время и город)
                    {
                        string time = infoParts[i];
                        string city = infoParts[i + 1];

                        labelText += time + " " + city + Environment.NewLine; // Добавляем время и город в labelText с переводом строки
                    }

                    label1.Text = labelText; // Устанавливаем labelText в label
                }
            }
        }*/


        private void button1_Click(object sender, EventArgs e)
        {
            string filterText = textBox1.Text.ToLower();

            var filteredData = (from stopoveR in db.Stopover
                                join village in db.Village on stopoveR.Village_ID equals village.Village_ID
                                join rout in db.Rout on stopoveR.Routes_ID equals rout.Rout_ID
                                join town in db.Town on rout.Town_ID equals town.Town_ID
                                where village.Village_Name.ToLower().Contains(filterText) || town.Town1.ToLower().Contains(filterText)
                                select new
                                {
                                    Routes_ID = rout.Rout_ID,
                                    Routes_Number = rout.Routes_number,
                                    Departure = rout.Otk,
                                    // Departure_Time = rout.Vrem_otp,
                                    Destination = town.Town1,
                                    //Destination_Time = rout.Vrem_Prib,
                                    Koll_Biletov = rout.Kol,
                                    Price = stopoveR.Price
                                }).ToList();

            dataGridView1.DataSource = filteredData;

        }
       
   

        private void button2_Click(object sender, EventArgs e)
        {
            /*DataGridViewRow curRow = dataGridView1.CurrentRow;
            int Routes_Id = Convert.ToInt32(curRow.Cells["Routes_ID"].Value);
            int Routes_Number = (int)curRow.Cells["Routes_Number"].Value;
            decimal Pric = Convert.ToDecimal(curRow.Cells["Price"].Value);
            string Koli = textBox2.Text;

            Kor BiletToKorzina = new Kor();
            BiletToKorzina.Id = Routes_Id;
            BiletToKorzina.Routes_Number = Routes_Number;
            BiletToKorzina.Data =Koli;
            BiletToKorzina.C_Departure = 3;
            BiletToKorzina.C_Departure_Time = "hd";
            BiletToKorzina.C_Destination = 2;
            BiletToKorzina.C_Destination_Time = "fhc";
            BiletToKorzina.Price = Pric;



            db.Kor.Add(BiletToKorzina);

            var korzinas = from korzina in db.Kor
                           join rout in db.Rout on korzina.Routes_Number equals rout.Rout_ID
                           
                           select new
                           {
                               Id=rout.Rout_ID,
                               Routes_Number = rout.Routes_number,
                              
                               Data = Koli,
                              
                           };

            dataGridView2.DataSource = korzinas.ToList();
*/          DataGridViewRow curRow = dataGridView1.CurrentRow;
            int Routes_ID = (int)curRow.Cells["Routes_ID"].Value;
            int Routes_Number = (int)curRow.Cells["Routes_Number"].Value;
            string data = textBox2.Text;
            
            MessageBox.Show(Routes_ID.ToString());
            MessageBox.Show(data.ToString());

            /*if (dataGridView3.CurrentCell != null) // Проверяем, что есть выбранная ячейка
            {
                int rowIndex = dataGridView3.CurrentCell.RowIndex;
                int columnIndex = dataGridView3.CurrentCell.ColumnIndex;
                
                int Routes_ID = (int)curRow.Cells["Menu_ID"].Value;

                // Выполняем действия в зависимости от индекса столбца
                if (columnIndex == 0) // Пример для первого столбца
                {
                    MessageBox.Show($"Выбрана строка с id: {rowIndex}");
                }
                else if (columnIndex == 1) // Пример для второго столбца
                {
                    // Код для второго столбца
                }
                // И так далее для других столбцов
            }
            else
            {
                MessageBox.Show("Ячейка не выбрана");
            }*/
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Убедимся, что была кликнута не заголовочная ячейка
            {
                int routesID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Routes_Number"].Value); // Получаем номер маршрута из выбранной строки

                var selectedRoutes = db.Rout.FirstOrDefault(m => m.Routes_number == routesID); // Находим соответствующую запись в базе данных

                if (selectedRoutes != null)
                {
                    string inf = selectedRoutes.Info; // Получаем информацию о маршруте
                    string[] infoParts = inf.Split(' '); // Разбиваем информацию на части

                    string labelText = ""; // Подготовим текст для label
                    for (int i = 0; i < infoParts.Length - 1; i += 2) // Теперь используем шаг 2, так как у нас две части информации (время и город)
                    {
                        string time = infoParts[i];
                        string city = infoParts[i + 1];

                        labelText += time + " " + city + Environment.NewLine; // Добавляем время и город в labelText с переводом строки
                    }

                    label1.Text = labelText; // Устанавливаем labelText в label
                }
            }
        }

      
    }
}

