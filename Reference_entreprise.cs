using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Projet_2_GoGreen
{
    internal class Reference_entreprise
    {
        private int id_key;
        private string label;
        public static Dictionary<int, Reference_entreprise> list_id_reference = new Dictionary<int, Reference_entreprise>();
        public static Dictionary<string, Reference_entreprise> list_label_reference = new Dictionary<string, Reference_entreprise>();

        ConnectDB conx = new ConnectDB();

        public Reference_entreprise(int id_key, string label)
        {
            this.id_key = id_key;
            this.label = label;
        }

        //private void listReference()
        //{

        //    var cmd = conx.createRequest("Select * from reference_entreprise;");
        //    var reader = cmd.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        int id = (int)reader["id"];
        //        string label = (string)reader["label"];
        //        list_id_reference.Add(id, new Reference_entreprise(id, label));
        //        list_label_reference.Add(label, new Reference_entreprise(id, label));
        //    }

        //    reader.Close();

        //    for (int i=0; i<list_id_reference.Count; i++)
        //        Console.Out.WriteLine(list_id_reference[i]);
            
        //}

        public Reference_entreprise createReference(int id_key, string label_ref)
        {
            Reference_entreprise reference = null;

            if (!list_id_reference.ContainsKey(id_key) && !list_label_reference.ContainsKey(label_ref))
            {
                reference = new Reference_entreprise(id_key, label_ref);
                list_id_reference.Add(id_key, reference);
                list_label_reference.Add(label_ref, reference);
            }
            else
            {
                MessageBox.Show("La réference que vous avez entré n'existe pas! \nMerci de contacter votre prestataire.","ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return reference;
        }

    }
}
