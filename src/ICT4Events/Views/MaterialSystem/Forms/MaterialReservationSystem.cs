﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Data.OracleContexts;
using SharedModels.Logic;
using SharedModels.Models;

namespace ICT4Events.Views.MaterialSystem.Forms
{
    public partial class MaterialReservationSystem : Form
    {
        #region Local Variables

        private readonly Event _event;
        private readonly Guest _guest;

        #endregion


        public MaterialReservationSystem(Event ev, Guest guest)
        {
            InitializeComponent();
            _event = ev;
            _guest = guest;
        }

        private void reserveBtn_Click(object sender, EventArgs e)
        {
            if (lsbUserMaterials.SelectedIndex == -1 || lsbUserMaterials.Items.Count <= 0) return;

            foreach (
                var material in
                    LogicCollection.MaterialLogic.GetAllByEventAndNonReserved(_event)
                        .Where(material => material.Name == lsbUserMaterials.SelectedItem.ToString()))
            {
                LogicCollection.MaterialLogic.AddReservation(material, _guest.ID, _event.StartDate, _event.EndDate);
            }

            UpdateListBox();
        }


        private void UpdateListBox()
        {
            lsbUserMaterials.Items.Clear();
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                foreach (var material in LogicCollection.MaterialLogic.GetAllByEventAndNonReserved(_event))
                {
                    lsbUserMaterials.Items.Add(material.Name);
                }
            }
            else
            {
                foreach (var material in LogicCollection.MaterialLogic.GetAllByEventAndNonReserved(_event).Where(material => txtSearch.Text == material.Name))
                {
                    lsbUserMaterials.Items.Add(material.Name);
                }
            }
        }

        private void MaterialReservationSystem_Load(object sender, EventArgs e)
        {
            UpdateListBox();
        }

        private void searchTb_TextChanged(object sender, EventArgs e)
        {
            UpdateListBox();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            UpdateListBox();
        }
    }
}