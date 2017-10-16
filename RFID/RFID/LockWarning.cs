using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RFID_Example {
	public partial class LockWarning : Form {
		private Form1 mainForm = null;

		public LockWarning() {
			InitializeComponent();
		}

		public LockWarning(Form callingForm) {
			mainForm = callingForm as Form1;
			InitializeComponent();
		}

		private void okBtn_Click(object sender, EventArgs e) {
			this.Hide();
			return;
		}

		private void cancelBtn_Click(object sender, EventArgs e) {
			//this.mainForm.lockCheck = false;
			this.Hide();
			return;
		}
	}
}
