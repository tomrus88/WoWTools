using System;
using System.Windows.Forms;
using WowTools.Core;

namespace UpdatePacketParser
{
    [Flags]
    public enum CustomFilterMask
    {
        CUSTOM_FILTER_NONE = 0,
        CUSTOM_FILTER_UNITS = 1,
        CUSTOM_FILTER_PETS = 2,
        CUSTOM_FILTER_VEHICLES = 4,
        CUSTOM_FILTER_OBJECTS = 8,
        CUSTOM_FILTER_TRANSPORT = 16,
        CUSTOM_FILTER_MO_TRANSPORT = 32
    };

    public partial class FilterForm : Form
    {
        public FilterForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mask = ObjectTypeMask.TYPEMASK_NONE;

            if (checkBox1.Checked)
                mask |= ObjectTypeMask.TYPEMASK_ITEM;

            if (checkBox2.Checked)
                mask |= ObjectTypeMask.TYPEMASK_CONTAINER;

            if (checkBox3.Checked)
                mask |= ObjectTypeMask.TYPEMASK_UNIT;

            if (checkBox4.Checked)
                mask |= ObjectTypeMask.TYPEMASK_PLAYER;

            if (checkBox5.Checked)
                mask |= ObjectTypeMask.TYPEMASK_GAMEOBJECT;

            if (checkBox6.Checked)
                mask |= ObjectTypeMask.TYPEMASK_DYNAMICOBJECT;

            if (checkBox7.Checked)
                mask |= ObjectTypeMask.TYPEMASK_CORPSE;

            var customMask = CustomFilterMask.CUSTOM_FILTER_NONE;

            if (checkBox8.Checked)
                customMask |= CustomFilterMask.CUSTOM_FILTER_TRANSPORT;

            if (checkBox9.Checked)
                customMask |= CustomFilterMask.CUSTOM_FILTER_PETS;

            if (checkBox10.Checked)
                customMask |= CustomFilterMask.CUSTOM_FILTER_VEHICLES;

            if (checkBox11.Checked)
                customMask |= CustomFilterMask.CUSTOM_FILTER_MO_TRANSPORT;

            if (checkBox12.Checked)
                customMask |= CustomFilterMask.CUSTOM_FILTER_UNITS;

            if (checkBox13.Checked)
                customMask |= CustomFilterMask.CUSTOM_FILTER_OBJECTS;

            ((FrmMain)Owner).PrintObjectType(mask, customMask);
        }
    }
}
