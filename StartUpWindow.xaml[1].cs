using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace PDFCreationApplication
{
    /// <summary>
    /// Interaction logic for DuplicateKeyCheck.xaml
    /// </summary>
    public partial class StartUpWindow : Window
    {
        #region Properties

        public static string emptyPathError { get; set; }

        public static string pathError { get; set; }

        public static string modeError { get; set; }

        public static string transformationCompleted { get; set; }

        public static List<string> clients { get; set; }

        public Transformation obj = new Transformation();

        public static string devFileName { get; set; }

        public static string qaFileName { get; set; }

        public static string prodFileName { get; set; }

        public static Dictionary<string, string> environments { get; set; }

        public static string targetLocation { get; set; }

        public static string targetLocationMsg { get; set; }

        public static string dirNotFoundEx { get; set; }
        public static string dirCannotBeDeletedEx { get; set; }
        public static string ioEx { get; set; }
        public static string argEx { get; set; }

        #endregion

        #region Constructors

        public StartUpWindow()
        {
            InitializeComponent();
            Initialize();
        }

        static StartUpWindow()
        {
            emptyPathError = "* Please enter the path(s)..";
            pathError = "* Please enter the correct directory path(s)..";
            transformationCompleted = "Transformation Completed....";
            targetLocationMsg = "* Please provide target location....";
            dirNotFoundEx = "The specified path is invalid/Path is on an unmapped drive !! Please provide correct path....";
            dirCannotBeDeletedEx = "The current directory(s) cannot be deleted !! Please delete it manually and retry....";
            ioEx = "Unable to create directory/file(s) !! Please check for directory privileges....";
            argEx = "The specified target path is null/empty !! Please provide a path....";

            clients = new List<string>();
            environments = new Dictionary<string, string>();

            devFileName = string.Empty;
            qaFileName = string.Empty;
            prodFileName = string.Empty;
        }

        #endregion

        #region Events

        private void cmbRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbClient.SelectedIndex > 0 && !cmbClient.SelectedItem.ToString().Equals("Select", StringComparison.InvariantCultureIgnoreCase))
            {
                checkBoxDev.IsEnabled = true;
                checkBoxQA.IsEnabled = true;
                checkBoxProd.IsEnabled = true;
                lblTargetLocation.Visibility = System.Windows.Visibility.Visible;
                txtTargetLocation.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Initialize();
            }
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            GetPDFFileNames();
        }

        private void txtFileName_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetEnvironmentSpecificFileNames();
        }

        private void btnDevFolderBrowserDailog_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();

            if (Directory.Exists(folderBrowserDialog.SelectedPath))
                txtDevPath.Text = folderBrowserDialog.SelectedPath;
        }

        private void btnQAFolderBrowserDailog_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();

            if (Directory.Exists(folderBrowserDialog.SelectedPath))
                txtQAPath.Text = folderBrowserDialog.SelectedPath;
        }

        private void btnProdFolderBrowserDailog_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();

            if (Directory.Exists(folderBrowserDialog.SelectedPath))
                txtProdPath.Text = folderBrowserDialog.SelectedPath;
        }

        private void txtPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CheckFilePathExists(txtDevPath.Text, txtProdPath.Text, txtQAPath.Text))
            {
                btnTransform.IsEnabled = true;
            }
            else
            {
                btnTransform.IsEnabled = false;
            }
        }

        private void btnTransform_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                environments.Clear();
                GetEnvironmentFileName();

                if (!string.IsNullOrEmpty(targetLocation) || !string.IsNullOrWhiteSpace(targetLocation))
                {
                    txtBlockErrorMsg.Visibility = System.Windows.Visibility.Hidden;

                    if (checkBoxDev.IsChecked.Value)
                    {
                        obj.Compare(txtDevPath.Text, txtQAPath.Text, cmbClient.SelectedItem.ToString(), environments, targetLocation);
                    }

                    if (checkBoxProd.IsChecked.Value)
                    {
                        obj.Compare(txtProdPath.Text, txtQAPath.Text, cmbClient.SelectedItem.ToString(), environments, targetLocation);
                    }

                    System.Windows.MessageBox.Show(transformationCompleted + Environment.NewLine + Environment.NewLine + string.Format("{0}{1}", "Target Location: ", targetLocation));
                    checkBoxForDuplicates.IsEnabled = true;
                }
                else
                {
                    txtBlockErrorMsg.Visibility = System.Windows.Visibility.Visible;
                    txtBlockErrorMsg.Text = targetLocationMsg;
                }
            }
            catch (DirectoryNotFoundException)
            {
                System.Windows.MessageBox.Show(string.Format("{0}", dirNotFoundEx), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException)
            {
                System.Windows.MessageBox.Show(string.Format("{0}", ioEx), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (UnauthorizedAccessException)
            {
                System.Windows.MessageBox.Show(string.Format("{0}", dirCannotBeDeletedEx), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentException)
            {
                System.Windows.MessageBox.Show(string.Format("{0}", argEx), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtTargetLocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTargetLocation.Text) || !string.IsNullOrEmpty(txtTargetLocation.Text))
            {
                targetLocation = txtTargetLocation.Text;
            }
            else
            {
                txtBlockErrorMsg.Text = targetLocationMsg;
            }
        }

        private void btnLocalPDFFileDailog_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
            fileDialog.ShowDialog();

            if (fileDialog.CheckFileExists)
                txtLocalPath.Text = fileDialog.FileName;
        }

        private void btnSVNReposotoryDailog_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();

            if (Directory.Exists(folderBrowserDialog.SelectedPath))
                txtSVNPath.Text = folderBrowserDialog.SelectedPath;
        }

        private void txtFilePath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CheckLocalPathExists(txtLocalPath.Text, txtSVNPath.Text))
            {
                btnCopyFiles.IsEnabled = true;
            }
            else
            {
                btnCopyFiles.IsEnabled = false;
            }
        }

        private void btnCopyFiles_Click(object sender, RoutedEventArgs e)
        {
            Enhancements.DirSearch(txtSVNPath.Text, txtLocalPath.Text);
            System.Windows.MessageBox.Show("Files Copied");
        }

        private void btnSelectPDFFileDailog_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
            fileDialog.ShowDialog();

            if (fileDialog.CheckFileExists)
                txtPDFFilePath.Text = fileDialog.FileName;
        }

        private void btnExcelFileDailog_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
            fileDialog.ShowDialog();

            if (fileDialog.CheckFileExists)
                txtExcelFilePath.Text = fileDialog.FileName;
        }

        private void txtUpdateValues_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CheckFilePathExists(txtPDFFilePath.Text, txtExcelFilePath.Text))
            {
                btnUpdateValues.IsEnabled = true;
            }
            else
            {
                btnUpdateValues.IsEnabled = false;
            }
        }

        private void btnUpdateValues_Click(object sender, RoutedEventArgs e)
        {
            Enhancements.ReplaceOldWithNewValue(txtPDFFilePath.Text, txtExcelFilePath.Text);
            System.Windows.MessageBox.Show("PDF Updated");
        }

        private void btnPDFFileDailog_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
            fileDialog.ShowDialog();

            if (fileDialog.CheckFileExists)
            {
                txtPDFPath.Text = fileDialog.FileName;
                btnCheckDuplicates.IsEnabled = true;
            }
            else
            {
                btnCheckDuplicates.IsEnabled = false;
            }
        }

        private void btnCheckDuplicates_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPDFPath.Text) || !string.IsNullOrWhiteSpace(txtPDFPath.Text))
            {
                CheckForDuplicates();
            }
            else
            {
                lblDuplicate.Content = "* Please select PDF file path..!!";
            }
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            GetClients();
            cmbClient.ItemsSource = clients;
            cmbClient.SelectedIndex = 0;
            cmbClient.IsEnabled = true;

            checkBoxDev.IsChecked = false;
            checkBoxDev.IsEnabled = false;
            checkBoxQA.IsChecked = false;
            checkBoxQA.IsEnabled = false;
            checkBoxProd.IsChecked = false;
            checkBoxProd.IsEnabled = false;

            lblDevFile.IsEnabled = false;
            txtDevPath.Text = string.Empty;
            txtDevPath.IsEnabled = false;
            btnDevFileDailog.IsEnabled = false;

            lblQAFile.IsEnabled = false;
            txtQAPath.Text = string.Empty;
            txtQAPath.IsEnabled = false;
            btnQAFileDailog.IsEnabled = false;

            lblProdFile.IsEnabled = false;
            txtProdPath.Text = string.Empty;
            txtProdPath.IsEnabled = false;
            btnProdFileDailog.IsEnabled = false;

            btnTransform.IsEnabled = false;

            txtBlockErrorMsg.Text = string.Empty;
            environments.Clear();
            checkBoxForDuplicates.IsEnabled = true;

            txtTargetLocation.Text = string.Empty;
            lblTargetLocation.Visibility = System.Windows.Visibility.Hidden;
            txtTargetLocation.Visibility = System.Windows.Visibility.Hidden;

            txtDevFileName.Text = string.Empty;
            txtQAFileName.Text = string.Empty;
            txtProdFileName.Text = string.Empty;

            //For Copying Files
            txtLocalPath.Text = string.Empty;
            txtSVNPath.Text = string.Empty;
            btnCopyFiles.IsEnabled = false;

            //For Updating Values
            txtPDFFilePath.Text = string.Empty;
            txtExcelFilePath.Text = string.Empty;
            btnUpdateValues.IsEnabled = false;
        }

        private void GetClients()
        {
            clients.Clear();

            XDocument clientFile = XDocument.Load(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Clients.xml"));

            if (clientFile != null)
            {
                clients.Add("Select");
                clients.AddRange(clientFile.Root.Elements().Select(x => x.Value).ToList());
            }
        }

        private void GetPDFFileNames()
        {
            if (checkBoxDev.IsChecked.Value)
            {
                txtDevFileName.Visibility = System.Windows.Visibility.Visible;
                checkBoxProd.IsEnabled = false;

                if (string.IsNullOrWhiteSpace(txtDevFileName.Text) && string.IsNullOrWhiteSpace(devFileName))
                {
                    devFileName = txtDevFileName.Text;
                }
                else
                {
                    txtDevFileName.Text = devFileName;
                }
            }
            else
            {
                txtDevFileName.Visibility = System.Windows.Visibility.Hidden;
                checkBoxProd.IsEnabled = true;
            }

            if (checkBoxQA.IsChecked.Value)
            {
                txtQAFileName.Visibility = System.Windows.Visibility.Visible;

                if (string.IsNullOrWhiteSpace(txtQAFileName.Text) && string.IsNullOrWhiteSpace(qaFileName))
                {
                    qaFileName = txtQAFileName.Text;
                }
                else
                {
                    txtQAFileName.Text = qaFileName;
                }
            }
            else
            {
                txtQAFileName.Visibility = System.Windows.Visibility.Hidden;
            }

            if (checkBoxProd.IsChecked.Value)
            {
                txtProdFileName.Visibility = System.Windows.Visibility.Visible;
                checkBoxDev.IsEnabled = false;

                if (string.IsNullOrWhiteSpace(txtProdFileName.Text) && string.IsNullOrWhiteSpace(prodFileName))
                {
                    prodFileName = txtProdFileName.Text;
                }
                else
                {
                    txtProdFileName.Text = prodFileName;
                }
            }
            else
            {
                txtProdFileName.Visibility = System.Windows.Visibility.Hidden;
                checkBoxDev.IsEnabled = true;
            }

            if (!checkBoxDev.IsChecked.Value && !checkBoxProd.IsChecked.Value && !checkBoxQA.IsChecked.Value)
            {
                txtDevPath.Text = string.Empty;
                txtDevPath.IsEnabled = false;
                lblDevFile.IsEnabled = false;
                txtProdPath.Text = string.Empty;
                txtProdPath.IsEnabled = false;
                lblProdFile.IsEnabled = false;
                txtQAPath.IsEnabled = false;
                txtQAPath.Text = string.Empty;
                lblQAFile.IsEnabled = false;
                txtBlockErrorMsg.Text = string.Empty;
                btnTransform.IsEnabled = false;
            }
        }

        private void GetEnvironmentSpecificFileNames()
        {
            if (GetEnvironment())
            {
                txtDevPath.Text = string.Empty;
                txtProdPath.Text = string.Empty;
                txtQAPath.Text = string.Empty;
            }
            else
            {
                txtDevPath.Text = string.Empty;
                txtDevPath.IsEnabled = false;
                lblDevFile.IsEnabled = false;
                btnDevFileDailog.IsEnabled = false;
                txtQAPath.IsEnabled = false;
                txtQAPath.Text = string.Empty;
                lblQAFile.IsEnabled = false;
                btnQAFileDailog.IsEnabled = false;
                txtProdPath.Text = string.Empty;
                txtProdPath.IsEnabled = false;
                lblProdFile.IsEnabled = false;
                btnProdFileDailog.IsEnabled = false;
                btnTransform.IsEnabled = false;
                txtBlockErrorMsg.Text = string.Empty;
            }
        }

        private bool GetEnvironment()
        {
            if ((checkBoxDev.IsChecked.Value || !checkBoxDev.IsChecked.Value) || (checkBoxQA.IsChecked.Value || !checkBoxQA.IsChecked.Value) || (checkBoxProd.IsChecked.Value || !checkBoxProd.IsChecked.Value))
            {
                if (checkBoxDev.IsChecked.Value)
                {
                    if (!string.IsNullOrWhiteSpace(txtDevFileName.Text))
                    {
                        devFileName = txtDevFileName.Text;

                        lblDevFile.IsEnabled = true;
                        txtDevPath.IsEnabled = true;
                        btnDevFileDailog.IsEnabled = true;
                        lblQAFile.IsEnabled = true;
                        txtQAPath.IsEnabled = true;
                        btnQAFileDailog.IsEnabled = true;

                        checkBoxProd.IsChecked = false;
                        checkBoxProd.IsEnabled = false;

                        return true;
                    }
                    else
                    {
                        lblDevFile.IsEnabled = false;
                        btnDevFileDailog.IsEnabled = false;
                        txtDevPath.IsEnabled = false;
                        lblQAFile.IsEnabled = false;
                        txtQAPath.IsEnabled = false;
                        btnQAFileDailog.IsEnabled = false;
                        checkBoxProd.IsEnabled = true;
                    }
                }
                else if (!checkBoxProd.IsChecked.Value)
                {
                    return false;
                }

                if (checkBoxQA.IsChecked.Value)
                {
                    if (!string.IsNullOrWhiteSpace(txtQAFileName.Text))
                    {
                        qaFileName = txtQAFileName.Text;
                    }
                }

                if (checkBoxProd.IsChecked.Value)
                {
                    if (!string.IsNullOrWhiteSpace(txtProdFileName.Text))
                    {
                        prodFileName = txtProdFileName.Text;

                        lblProdFile.IsEnabled = true;
                        txtProdPath.IsEnabled = true;
                        btnProdFileDailog.IsEnabled = true;
                        lblQAFile.IsEnabled = true;
                        txtQAPath.IsEnabled = true;
                        btnQAFileDailog.IsEnabled = true;

                        checkBoxDev.IsChecked = false;
                        checkBoxDev.IsEnabled = false;

                        return true;
                    }
                    else
                    {
                        lblProdFile.IsEnabled = false;
                        txtProdPath.IsEnabled = false;
                        btnProdFileDailog.IsEnabled = true;
                        lblQAFile.IsEnabled = false;
                        txtQAPath.IsEnabled = false;
                        btnQAFileDailog.IsEnabled = true;
                        checkBoxDev.IsEnabled = true;
                    }
                }
                else if (!checkBoxDev.IsChecked.Value)
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckFilePathExists(string devPath, string prodPath, string qaPath)
        {
            if (!string.IsNullOrWhiteSpace(txtDevPath.Text) && !string.IsNullOrWhiteSpace(txtQAPath.Text))
            {
                if (Directory.Exists(devPath) && Directory.Exists(qaPath))
                {
                    txtBlockErrorMsg.Visibility = System.Windows.Visibility.Hidden;
                    txtBlockErrorMsg.Text = string.Empty;
                    return true;
                }
                else
                {
                    return SetErrorMessage(pathError);
                }
            }
            else if (!string.IsNullOrWhiteSpace(txtProdPath.Text) && !string.IsNullOrWhiteSpace(txtQAPath.Text))
            {
                if (Directory.Exists(prodPath) && Directory.Exists(qaPath))
                {
                    txtBlockErrorMsg.Visibility = System.Windows.Visibility.Hidden;
                    txtBlockErrorMsg.Text = string.Empty;
                    return true;
                }
                else
                {
                    return SetErrorMessage(pathError);
                }
            }
            else
            {
                return SetErrorMessage(emptyPathError);
            }
        }

        private bool SetErrorMessage(string errorMsg)
        {
            txtBlockErrorMsg.Visibility = System.Windows.Visibility.Visible;
            txtBlockErrorMsg.Text = errorMsg;
            return false;
        }

        private bool CheckLocalPathExists(string localPath, string svnPath)
        {
            if (!string.IsNullOrWhiteSpace(txtLocalPath.Text) && !string.IsNullOrWhiteSpace(txtSVNPath.Text))
            {
                if (File.Exists(localPath) && Directory.Exists(svnPath))
                {
                    txtBlockErrorMsg_1.Visibility = System.Windows.Visibility.Hidden;
                    txtBlockErrorMsg_1.Text = string.Empty;
                    return true;
                }
                else
                {
                    return SetErrorMessage_1(pathError);
                }
            }
            else
            {
                return SetErrorMessage_1(emptyPathError);
            }
        }

        private bool SetErrorMessage_1(string errorMsg)
        {
            txtBlockErrorMsg_1.Visibility = System.Windows.Visibility.Visible;
            txtBlockErrorMsg_1.Text = errorMsg;
            return false;
        }

        private bool CheckFilePathExists(string pdfFilePath, string excelFilePath)
        {
            if (!string.IsNullOrWhiteSpace(txtPDFFilePath.Text) && !string.IsNullOrWhiteSpace(txtExcelFilePath.Text))
            {
                if (File.Exists(pdfFilePath) && File.Exists(excelFilePath))
                {
                    txtBlockErrorMsg_2.Visibility = System.Windows.Visibility.Hidden;
                    txtBlockErrorMsg_2.Text = string.Empty;
                    return true;
                }
                else
                {
                    return SetErrorMessage_2(pathError);
                }
            }
            else
            {
                return SetErrorMessage_2(emptyPathError);
            }
        }

        private bool SetErrorMessage_2(string errorMsg)
        {
            txtBlockErrorMsg_2.Visibility = System.Windows.Visibility.Visible;
            txtBlockErrorMsg_2.Text = errorMsg;
            return false;
        }

        private void GetEnvironmentFileName()
        {
            if (checkBoxDev.IsChecked.Value)
            {
                if (!string.IsNullOrEmpty(txtDevFileName.Text) || !string.IsNullOrWhiteSpace(txtDevFileName.Text))
                    environments.Add("DEV", txtDevFileName.Text);
            }

            if (checkBoxQA.IsChecked.Value)
            {
                if (!string.IsNullOrEmpty(txtQAFileName.Text) || !string.IsNullOrWhiteSpace(txtQAFileName.Text))
                    environments.Add("QA", txtQAFileName.Text);
            }

            if (checkBoxProd.IsChecked.Value)
            {
                if (!string.IsNullOrEmpty(txtProdFileName.Text) || !string.IsNullOrWhiteSpace(txtProdFileName.Text))
                    environments.Add("PROD", txtProdFileName.Text);
            }
        }

        private class KeyvaluePairs
        {
            public string key { get; set; }
            public string value { get; set; }
        }

        private void CheckForDuplicates()
        {
            IEnumerable<XElement> pdfKeys = null;
            List<KeyvaluePairs> duplicateKeys = new List<KeyvaluePairs>();
            Dictionary<string, string> keys = new Dictionary<string, string>();
            dgDuplicateKeys.ItemsSource = null;
            try
            {
                XDocument pdfFile = XDocument.Load(txtPDFPath.Text);

                pdfKeys = pdfFile.Root.Descendants().FirstOrDefault().Descendants();

                foreach (var key in pdfKeys)
                {
                    try
                    {
                        keys.Add(key.Attribute("key").Value, key.Value);
                    }
                    catch (Exception ex)
                    {
                        duplicateKeys.Add(new KeyvaluePairs() { key = key.Attribute("key").Value, value = key.Value });
                    }
                }

                if (duplicateKeys.Any())
                {
                    lblDuplicate.Content = "* Duplicates Occured";
                    dgDuplicateKeys.Visibility = System.Windows.Visibility.Visible;
                    dgDuplicateKeys.ItemsSource = duplicateKeys;
                }
                else
                {
                    lblDuplicate.Content = "* No Duplicates Occured";
                    dgDuplicateKeys.Visibility = System.Windows.Visibility.Hidden;
                }

                System.Windows.MessageBox.Show("Check Completed");
            }
            catch (DirectoryNotFoundException)
            {
                System.Windows.MessageBox.Show(string.Format("{0}", "The specified file path is invalid !! Please provide correct path...."), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
    }
}