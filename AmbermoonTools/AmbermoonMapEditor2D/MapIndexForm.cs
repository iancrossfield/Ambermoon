﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AmbermoonMapEditor2D
{
    public partial class MapIndexForm : Form
    {
        readonly Dictionary<uint, string> tooltips;
        internal uint MapIndex => numericUpDownMapIndex.Visible ? (uint)numericUpDownMapIndex.Value : (uint)comboBoxMapIndices.Items[comboBoxMapIndices.SelectedIndex];

        public MapIndexForm(Dictionary<uint, string> mapIndices)
        {
            tooltips = mapIndices;
            InitializeComponent();

            comboBoxMapIndices.Location = numericUpDownMapIndex.Location;

            if (mapIndices == null || mapIndices.Count == 0)
            {
                numericUpDownMapIndex.Visible = true;
                comboBoxMapIndices.Visible = false;
            }
            else
            {
                foreach (var mapIndex in mapIndices)
                    comboBoxMapIndices.Items.Add(mapIndex.Key);

                numericUpDownMapIndex.Visible = false;
                comboBoxMapIndices.Visible = true;
                comboBoxMapIndices.SelectedIndex = 0;
            }
        }

        private void MapIndexForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("Do you really want to cancel map loading?", "Cancel map loading", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    e.Cancel = true;
            }
        }

        private void comboBoxMapIndices_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            string text = comboBoxMapIndices.GetItemText(comboBoxMapIndices.Items[e.Index]);
            e.DrawBackground();
            using var brush = new SolidBrush(e.ForeColor);
            e.Graphics.DrawString(text, e.Font, brush, e.Bounds);
            if (comboBoxMapIndices.DroppedDown && (e.State & DrawItemState.Selected) == DrawItemState.Selected && !string.IsNullOrWhiteSpace(tooltips[(uint)comboBoxMapIndices.Items[e.Index]]))
                toolTipMapIndex.Show(tooltips[(uint)comboBoxMapIndices.Items[e.Index]], comboBoxMapIndices, e.Bounds.Right, e.Bounds.Bottom);
            e.DrawFocusRectangle();
        }

        private void comboBoxMapIndices_DropDownClosed(object sender, System.EventArgs e)
        {
            toolTipMapIndex.Hide(comboBoxMapIndices);
        }

        private void MapIndexForm_Load(object sender, System.EventArgs e)
        {
            comboBoxMapIndices.DrawMode = DrawMode.OwnerDrawFixed;
        }

        private void comboBoxMapIndices_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tooltips[MapIndex]))
                Text = $"Enter map index - {MapIndex}";
            else
                Text = $"Enter map index - {MapIndex}: {tooltips[MapIndex]}";
        }

        private void numericUpDownMapIndex_ValueChanged(object sender, System.EventArgs e)
        {
            Text = $"Enter map index - {MapIndex}";
        }
    }
}