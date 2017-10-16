/* - RFID full -
 * This example displays a gui that shows the attached Phidgets RFID reader device's details and will
 * display and tag data that is scanned by the antenna.  it also provides checkboxes to manipulate the
 * different attirbutes of the RFID reader as well as the digital outputs.
 *
 * Please note that this example was designed to work with only one Phidget RFID connected.
 * For an example showing how to use two Phidgets of the same time concurrently, please see the
 * Servo-multi example in the Servo Examples.
 *
 * Copyright 2007 Phidgets Inc.
 * This work is licensed under the Creative Commons Attribution 2.5 Canada License.
 * To view a copy of this license, visit http://creativecommons.org/licenses/by/2.5/ca/
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Phidget22;
using Phidget22.Events;
using Phidget22.ExampleUtils;
using System.Diagnostics;
using NetworkIt;
using System.Media;

namespace RFID_Example {
	public partial class Form1 : Form {
		CommandLineOpen open;
        formCleaner cleaner;
        private RFID rfid; //Declare an RFID object
		private ErrorEventBox errorBox;
        private Random r = new Random();
		Boolean runonce = false;
        string currentRFIDTag;
		LockWarning warningForm;
        private Dictionary<string, girlscout_memory>dict= new Dictionary<string,girlscout_memory>();
        private SoundPlayer soundtoPlay =
            new SoundPlayer("audio\\awesome.wav");
        private string nameOfGirl = "Megan";
        Client client;
        public Form1() {
			open = new CommandLineOpen(this);
            cleaner = new formCleaner(this);
           
            InitializeComponent();
            dict.Add("0f00aeafd5", new girlscout_memory(nameOfGirl, "November 7, 2004", "Good job Sasha!"));
            dict.Add("10000cfe72", new girlscout_memory(nameOfGirl, "January 10, 2001", "Continue to be a great citizen in honor of the Girl Scouts"));
            dict.Add("10000cec87", new girlscout_memory(nameOfGirl, "December 21, 2013", "You did a fantastic job with this goal. Thanks for all your help!"));
            dict.Add("10000d02ba", new girlscout_memory(nameOfGirl, "January 10, 2016", "Im glad the bugs didnt bug you lol. I hope you continue your passion with learning about nature!"));
            dict.Add("1000252685", new girlscout_memory(nameOfGirl, 80));
            dict.Add("0f00aee927", new girlscout_memory(nameOfGirl, "March 19, 2001", "It was a pleasure teaching you youngster"));
            dict.Add("0102ac381a", new girlscout_memory(nameOfGirl, 50));
            initiateNetworkContact();
        }

		public Form1(String[] commandLine) {
			open = new CommandLineOpen(this);
            cleaner = new formCleaner(this);
            open.commandLine = commandLine;
			InitializeComponent();
		}
       
		//initialize our Phidgets RFID reader and hook the event handlers
		private void Form1_Load(object sender, EventArgs e) {
			//writeProtoCmb.DataSource = typeof(RFIDProtocol).ToList();

			errorBox = new ErrorEventBox(this);
			rfid = new RFID();

			rfid.Attach += rfid_Attach;
			rfid.Detach += rfid_Detach;
			rfid.Error += rfid_Error;

			rfid.Tag += rfid_Tag;
			rfid.TagLost += rfid_TagLost;

			open.openCmdLine(rfid);

		}

		//attach event handler..populate the details fields as well as display the attached status.  enable the checkboxes to change
		//the values of the attributes of the RFID reader such as enable or disable the antenna and onboard led.
		void rfid_Attach(object sender, AttachEventArgs e) {
			//phidgetInfoBox1.FillPhidgetInfo((Phidget)sender);
			//readBox.Visible = true;

			RFID attachedDevice = (RFID)sender;

			//switch (attachedDevice.DeviceID) {
			//case DeviceID.PN_1024:
			//	writeBox.Visible = true;
			//	break;
			//case DeviceID.PN_1023:
			//	writeBox.Visible = false;
			//	break;
			//}

			//antennaChk.Visible = true;
			//keyboardCheckBox.Visible = true;
			//antennaChk.Checked = true;
            try {
                rfid.AntennaEnabled = true;
            }
            catch(PhidgetException ex) { errorBox.addMessage("Error enabling antenna: " + ex.Message); }
		}

		//detach event handler...clear all the fields, display the attached status, and disable the checkboxes.
		void rfid_Detach(object sender, DetachEventArgs e) {
			//phidgetInfoBox1.Clear();
            cleaner.clean();

            //writeBox.Visible = false;
			//antennaChk.Visible = false;
			//keyboardCheckBox.Visible = false;
			//readBox.Visible = false;
			runonce = false;
		}

		void rfid_Error(object sender, ErrorEventArgs e) {
			errorBox.addMessage(e.Description);
		}

		//Tag event handler...we'll display the tag code in the field on the GUI
		void rfid_Tag(object sender, RFIDTagEventArgs e) {
			//tagTxt.Text = e.Tag;
			//protoTxt.Text = e.Protocol.GetDescription();
            if (dict.ContainsKey(e.Tag))
            {
                issued_to_box.Text = dict[e.Tag].issued_to;
                issue_date_box.Text = dict[e.Tag].issue_date;
                destinationBox.Text = dict[e.Tag].special_msg;
                if (dict[e.Tag].issue_date!=null)
                {
                    progressBar.Value = 100;
                }
                else
                {
                    progressBar.Value = dict[e.Tag].progress;
                }
            }
            currentRFIDTag = e.Tag;
            sendMessage(progressBar.Value);
            if(progressBar.Value >=0 && progressBar.Value <=50)
            {
                int random = RandomInt(1,2);
                if (random==1)
                {
                    soundtoPlay = new SoundPlayer("audio\\onestepatatime.wav");
                }
                if (random==2)
                {
                    soundtoPlay = new SoundPlayer("audio\\justalittleways.wav");
                }
                Debug.WriteLine(random);
                
                soundtoPlay.Play();
            }
            else
            {
                int random = RandomInt(1, 2);
                if (dict[e.Tag].calc_Year() > 2014) //badge is 3 years old
                {
                    if (random == 1)
                    {
                        soundtoPlay = new SoundPlayer("audio\\youre_fantastic.wav");
                    }
                    if (random == 2)
                    {
                        soundtoPlay = new SoundPlayer("audio\\good_job.wav");
                    }
                    
                }
                else
                {
                    //if (random == 1)
                    //{
                        soundtoPlay = new SoundPlayer("audio\\whatsnew.wav");
                    //}
                    //if (random == 2)
                    //{
                    //    soundtoPlay = new SoundPlayer("audio\\good_job.wav");
                    //}
                }
                soundtoPlay.Play();
                
            }
            
			//This sends the RFID tag and an enter to the active application
			//if (keyboardCheckBox.Checked == true) {
			//	SendKeys.Send(e.Tag);
			//	SendKeys.Send("{ENTER}");
			//}
		}

		//Tag lost event handler...here we simply want to clear our tag field in the GUI
		void rfid_TagLost(object sender, RFIDTagLostEventArgs e) {
            //tagTxt.Text = "";
            //protoTxt.Text = "";
            issued_to_box.Text = "";
            issue_date_box.Text = "";
            destinationBox.Text = "";
            progressBar.Value = 0;
        }

		//Enable or disable the RFID antenna by clicking the checkbox
		//private void antennaChk_CheckedChanged(object sender, EventArgs e) {
		//	//try {
		//	//	rfid.AntennaEnabled = antennaChk.Checked;
		//	//} catch (PhidgetException ex) {
		//	//	errorBox.addMessage("Error enabling antenna: " + ex.Message);
		//	//}
		//}

		//When the application is being terminated, close the Phidget.
		private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
			rfid.Close();

			if (warningForm != null)
				warningForm.Close();
		}

		//private void writeBtn_Click(object sender, EventArgs e) {
		//	try {
		//		RFIDProtocol proto = (RFIDProtocol)Enum.Parse(typeof(RFIDProtocol), writeProtoCmb.SelectedValue.ToString());
		//		//rfid.Write(writeTagTxt.Text, proto, writeLockChk.Checked);
		//	} catch (PhidgetException ex) {
		//		errorBox.addMessage("Error writing tag: " + ex.Message);
		//	}
		//}

		private void writeLockChk_CheckedChanged(object sender, EventArgs e) {
			//let the user know that this will permanently lock the RFID tag making it no longer writable
			//if (writeLockChk.Checked == true && runonce == false) {
			//	warningForm = new LockWarning(this);
			//	warningForm.ShowDialog();
			//	runonce = true;
			//}
		}

		//public Boolean lockCheck {

		//	get { return writeLockChk.Checked; }
		//	set { writeLockChk.Checked = value; }
		//}

        private void writeButton2_Click(object sender, EventArgs e)
        {
            //if (!dict.ContainsKey(currentRFIDTag))
            //{
            //    dict.Add(currentRFIDTag, destinationBox.Text);
            //}
            //else
            //{
            //    dict[currentRFIDTag] = destinationBox.Text;
            //}
            //destinationBox.Text = dict[currentRFIDTag];
            //Debug.WriteLine(dict[currentRFIDTag]);
        }

        class girlscout_memory
        {
            public girlscout_memory()
            { }
            public girlscout_memory(string issued_to, string issue_date, string special_msg)
            {
                this.issued_to = issued_to;
                this.issue_date = issue_date;
                this.special_msg = special_msg;

            }
            public girlscout_memory(string issued_to, int progress)
            {
                this.progress = progress;
                this.issued_to = issued_to;
            }
            public string issued_to { get; set; }
            public string issue_date { get; set; }
            public string special_msg { get; set; }
            public int progress { get; set; }
            public int calc_Year()
            {
                DateTime datevalue = (Convert.ToDateTime(issue_date.ToString()));
                return (int)datevalue.Year;
            }
        }

        private void initiateNetworkContact()
        {
            client = new Client("dragonfruit", "http://581.cpsc.ucalgary.ca", 8000);
            client.MessageReceived += Client_MessageReceived;
            client.Connected += Client_Connected;
            client.StartConnection();
        }
        private void sendMessage(int progress)
        {
            NetworkIt.Message m = new NetworkIt.Message("progress");
            m.DeliverToSelf = false;
            m.AddField("progress", "" + progress);
            //m.AddField("count", "" + messageCount++);

            client.SendMessage(m);
        }

        private void Client_Connected(object sender, EventArgs e)
        {
            Debug.WriteLine("Connection Successful");
        }

        private void Client_MessageReceived(object sender, NetworkItMessageEventArgs e)
        {
            int random = RandomInt(1, 2);
            Debug.WriteLine(e.ReceivedMessage.ToString());
            NetworkIt.Message m = e.ReceivedMessage;
            if (m.Subject.Equals("force"))
            {
                int power = 0;
                
                int.TryParse(m.GetField("message"), out power);
                Debug.WriteLine("power" +power);
               if (power>=1 && power <=2)
                {
                    Debug.WriteLine("light squeeze");
                    if (random == 1)
                    {
                        soundtoPlay = new SoundPlayer("audio\\whyhellothere.wav");
                        soundtoPlay.Play();
                    }
                    else
                    {
                        soundtoPlay = new SoundPlayer("audio\\caliente.wav");
                        soundtoPlay.Play();
                    }
                }
                else if (power==3)
                {
                    Debug.WriteLine("medium squeeze");
                    if (random == 1)
                    {
                        soundtoPlay = new SoundPlayer("audio\\letsgo.wav");
                        soundtoPlay.Play();
                    }
                    else if (random==2)
                    {
                        soundtoPlay = new SoundPlayer("audio\\onestepatatime.wav");
                        soundtoPlay.Play();
                    }
                    
                    
                }
                else if (power ==4)
                {
                    random = RandomInt(1, 3);
                    Debug.WriteLine("big squeeze");
                    if (random==1)
                    {
                        soundtoPlay = new SoundPlayer("audio\\ow_easy.wav");
                        soundtoPlay.Play();
                    }
                    else if (random == 2)
                    {
                        soundtoPlay = new SoundPlayer("audio\\whatstheproblem.wav");
                        soundtoPlay.Play();
                    }
                    else if (random == 3)
                    {
                        soundtoPlay = new SoundPlayer("audio\\cryout.wav");
                        soundtoPlay.Play();
                    }

                }
                
            }
        }
        private int RandomInt(int low, int high)
        {
            return (r.Next() % (high + 1) + low);
        }

    }
}