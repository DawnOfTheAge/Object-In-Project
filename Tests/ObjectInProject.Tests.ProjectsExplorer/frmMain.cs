using ObjectInProject.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ObjectInProject.Gui;
using Newtonsoft.Json;

namespace ObjectInProject.Tests.ProjectsExplorer
{
    public partial class frmMain : Form
    {
        #region Data Members

        private SearchProjects m_Configuration;

        private string m_FullFilename;

        #endregion

        #region Constructor

        public frmMain()
        {
            InitializeComponent();
        }

        #endregion

        #region Startup

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                Location = Cursor.Position;

                string m_Path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                m_FullFilename = $@"{m_Path}\Configuration\ObjectInProject.Configuration{ObjectInProjectConstants.JSON_EXTENSION}";

                if (!File.Exists(m_FullFilename))
                {
                    MessageBox.Show($"'{m_FullFilename}' Does Not Exist", "Configuration File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                string json = File.ReadAllText(m_FullFilename);
                SearchProjects m_Configuration = JsonConvert.DeserializeObject<SearchProjects>(json);

                if (m_Configuration == null)
                {
                    m_Configuration = new SearchProjects();
                }

                frmSearchProjectsTree searchProjectsTree = new frmSearchProjectsTree(m_Configuration, 
                                                                                     new List<Editors>() { Editors.Notepad, Editors.NotepadPlusPlus});
                searchProjectsTree.Message += SearchProjectsTree_Message;
                searchProjectsTree.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Gui

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            string result;

            try
            {
                DialogResult dialogResult = MessageBox.Show("Save Configuration?", "Save Configuration On Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    SaveConfiguration(out result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Closing Object In Project", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private bool SearchProjectsTree_Message(object message, out object reply, out string result)
        {
            result = string.Empty;

            reply = null;

            try
            {
                if (message == null)
                {
                    result = "Message Is Null";

                    return false;
                }

                MessageObject messageObject = (MessageObject)message;

                switch (messageObject.Action)
                {
                    case IoAction.Keep:
                    case IoAction.KeepAndSave:
                        if (messageObject.Data == null)
                        {
                            result = $"Message Data Is Null";

                            return false;
                        }

                        SearchProjects fromTreeSearchProjects = (SearchProjects)(messageObject.Data);

                        m_Configuration = fromTreeSearchProjects;

                        if (messageObject.Action == IoAction.KeepAndSave)
                        {
                            if (!SaveConfiguration(out result))
                            {
                                return false;
                            }
                        }
                        return true;

                    case IoAction.Reload:
                        if (!LoadConfiguration(out result))
                        {
                            return false;
                        }

                        reply = m_Configuration;
                        return true;

                    default:
                        result = $"Wrong Action[{messageObject.Action}]";
                        return false;
                }
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        #endregion

        #region Configuration

        private bool LoadConfiguration(out string result)
        {            
            string json;

            result = string.Empty;

            try
            {
                if (!File.Exists(m_FullFilename))
                {
                    result = "'" + m_FullFilename + "' Does Not Exist";

                    MessageBox.Show(result, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                json = File.ReadAllText(m_FullFilename);
                m_Configuration = JsonConvert.DeserializeObject<SearchProjects>(json);

                return (m_Configuration != null);
            }
            catch (Exception e)
            {
                result = e.Message;

                MessageBox.Show(result, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        private bool SaveConfiguration(out string result)
        {            
            try
            {
                return SaveConfiguration(m_Configuration, out result);
            }
            catch (Exception e)
            {
                result = e.Message;

                MessageBox.Show(result, "Save Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        private bool SaveConfiguration(SearchProjects searchProjects, out string result)
        {            
            result = "";

            try
            {
                if (searchProjects == null)
                {
                    MessageBox.Show("Configuration Is Null", "Save Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                string json = JsonConvert.SerializeObject(searchProjects);
                File.WriteAllText(m_FullFilename, json);

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                MessageBox.Show(result, "Save Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        #endregion
    }
}
