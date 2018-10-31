using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1
{
    public partial class Form1 : Form
    {

        List<Ownership> allOwners = new List<Ownership>();
        List<Players> allPlayers = new List<Players>();
        List<Pokemon> allPokemon = new List<Pokemon>();

        public Form1()
        {
            InitializeComponent();
            loadownershipData();
            loadplayerData();
            loadpokemonData();
            PopulateStudioCombo();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cities = comboBox1.SelectedItem.ToString();

            var players = from p in allPlayers
                          where p.city == cities
                          select new { p.nickname, p.paid };

            dataGridView4.AutoGenerateColumns = true;
            dataGridView4.DataSource = new BindingSource(players, null);



        }

   
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string nn = textBox3.Text;

                var owners = from p in allPlayers
                             from poke in allPokemon
                             from o in allOwners
                             where p.nickname == nn && p.ID == o.playerID && o.pokemonID == poke.ID
                             select new { p.nickname, poke.pokemonName, o.level, o.numberOwned };


                dataGridView4.AutoGenerateColumns = true;
                dataGridView4.DataSource = new BindingSource(owners, null);
            }
            catch
            {
                MessageBox.Show("Invalid");

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                string defenseVal = textBox2.Text;

                var pokemons = from poke in allPokemon
                               where poke.defense >= Convert.ToInt32(defenseVal)
                               orderby poke.defense descending
                               select new { poke.pokemonName, poke.defense };

                dataGridView4.AutoGenerateColumns = true;
                dataGridView4.DataSource = new BindingSource(pokemons, null);
            }

            catch
            {
                MessageBox.Show("Invalid");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                int attackVal = Convert.ToInt32(textBox1.Text);

                var pokemons = from poke in allPokemon
                               where poke.attack >= attackVal
                               orderby poke.attack descending
                               select new { poke.pokemonName, poke.attack };

                dataGridView4.AutoGenerateColumns = true;
                dataGridView4.DataSource = new BindingSource(pokemons, null);
            }
            catch
            {
                MessageBox.Show("Invalid");
            }

         
        }

        void loadownershipData()
        {

            string[] lines = Properties.Resources.Ownership.Trim().Split('\n');

            foreach (string line in lines)
            {

                string[] fields = line.Trim().Split(',');

                Ownership o = new Ownership
                {
                    playerID = Convert.ToInt32(fields[0]),
                    pokemonID = Convert.ToInt32(fields[1]),
                    level = Convert.ToInt32(fields[2]),
                    numberOwned = Convert.ToInt32(fields[3])

                };


                allOwners.Add(o);
            }
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = allOwners;


        }

        void loadplayerData()
        {

            string[] lines = Properties.Resources.Players.Trim().Split('\n');

            foreach (string line in lines)
            {

                string[] fields = line.Trim().Split(',');
                Players p = new Players
                {
                    name = fields[0],
                    ID = Convert.ToInt32(fields[1]),
                    nickname = fields[2],
                    city = fields[3], 
                    paid = Convert.ToBoolean(fields[4])
            };


                allPlayers.Add(p);
            }
            dataGridView2.AutoGenerateColumns = true;
            dataGridView2.DataSource = allPlayers;


        }

        void loadpokemonData()
        {

            string[] lines = Properties.Resources.Pokemon.Trim().Split('\n');

            foreach (string line in lines)
            {

                string[] fields = line.Trim().Split(',');
                Pokemon poke = new Pokemon
                {
                    ID = Convert.ToInt32(fields[0]),
                    pokemonName = fields[1],
                    attack = Convert.ToInt32(fields[2]),
                    defense = Convert.ToInt32(fields[3]),
                };


                allPokemon.Add(poke);
            }
            dataGridView3.AutoGenerateColumns = true;
            dataGridView3.DataSource = allPokemon;


        }

        
        void PopulateStudioCombo()
        {
            var cities = (from p in allPlayers select p.city).Distinct();
            //removed duplicates
            HashSet<string> set = new HashSet<string>(cities);

            comboBox1.Items.AddRange(set.ToArray());

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
