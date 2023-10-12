using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HoneyLovely.Models;

namespace HoneyLovely
{
    public partial class NewForm : Form
    {
        public Member CurrentMember { get; private set; } = new Member();

        public NewForm()
        {
            InitializeComponent();
            this.Load += (s, a) => InitializeDataBindings();
        }

        private void InitializeDataBindings()
        {
            this.txtName.DataBindings.Add(new Binding("Text", CurrentMember, "Name"));
            this.txtCardNo.DataBindings.Add(new Binding("Text", CurrentMember, "CardNo"));
            this.txtPhone.DataBindings.Add(new Binding("Text", CurrentMember, "Phone"));
            this.dtpBirthday.DataBindings.Add(new Binding("Value", CurrentMember, "Birthday"));

            for (int i = 0; i < this.combGender.Items.Count; i++)
            {
                if (string.Equals(CurrentMember.Gender, ((KeyValuePair<string, string>)this.combGender.Items[i]).Key))
                {
                    this.combGender.SelectedIndex = i;
                }
            }
            CurrentMember.PropertyChanged += (s, a) =>
            {
                if (string.Equals("Gender", a.PropertyName))
                {
                    for (int i = 0; i < this.combGender.Items.Count; i++)
                    {
                        if (string.Equals(CurrentMember.Gender, ((KeyValuePair<string, string>)this.combGender.Items[i]).Key))
                        {
                            this.combGender.SelectedIndex = i;
                        }
                    }
                }
            };
        }

        private void NewForm_Load(object sender, EventArgs e)
        {
            this.combGender.Items.Add(new KeyValuePair<string, string>("boy", "男"));
            this.combGender.Items.Add(new KeyValuePair<string, string>("girl", "女"));
            this.btnCancel.Click += (s, a) =>
            {
                this.Close();
                this.DialogResult = DialogResult.Cancel;
            };
            this.btnConfirm.Click += (s, a) =>
            {
                if (combGender.SelectedItem != null)
                {
                    this.CurrentMember.Gender = ((KeyValuePair<string, string>)combGender.SelectedItem).Key;
                }
                this.DialogResult = DialogResult.OK;
            };
        }
    }
}
