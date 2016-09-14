using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PDFCreationApplication
{
    public class Transformation
    {
        #region Properties

        public static string client { get; set; }

        public static string firstPath { get; set; }

        public static string secondPath { get; set; }

        public static XName nvPairs { get; set; }

        public static XName gvName { get; set; }

        public static XName gvValue { get; set; }

        public static XName bw { get; set; }

        public static XName bindings { get; set; }

        public static XName binding { get; set; }

        public static XName bindingType { get; set; }
        public static XName bindingVersion { get; set; }
        public static XName bindingLocation { get; set; }
        public static XName bindingStartOnBoot { get; set; }
        public static XName bindingEnableVerbose { get; set; }
        public static XName bindingMaxLogFileSize { get; set; }
        public static XName bindingMaxLogFileCount { get; set; }
        public static XName bindingThreadCount { get; set; }
        public static XName bindingInitHeapSize { get; set; }
        public static XName bindingMaxHeapSize { get; set; }
        public static XName bindingThreadStackSize { get; set; }
        public static XName bindingCheckpoint { get; set; }
        public static XName bindingTimeOut { get; set; }

        public static XName bwprocesses { get; set; }
        public static XName bwprocess { get; set; }

        public static string appendNVKeyText { get; set; }

        public static string appendBindingKeyText { get; set; }

        public static string pipe { get; set; }

        public static string pathSeperator { get; set; }

        public static string newKey { get; set; }

        public static string newGVValue { get; set; }

        public static string serviceFamilyName { get; set; }

        public static string serviceName { get; set; }

        public static string transformationPath { get; set; }

        public static string transformedServiceFamilyPath { get; set; }

        public static string transform { get; set; }

        public static string serviceNameComment { get; set; }

        public static string serviceFamilyNameComment { get; set; }

        public static string transformFileName { get; set; }

        public static bool serviceFamilyAdded { get; set; }

        public static bool serviceNameAdded { get; set; }

        public static Dictionary<string, string> originalNVPairs { get; set; }

        public static Dictionary<string, string> propertiesKeyValuePairs { get; set; }

        public static Dictionary<string, string> xmlKeyValuePairs { get; set; }

        public static Dictionary<string, string> originalBinding { get; set; }

        public static Dictionary<string, string> xmlBindingKeyValuePairs { get; set; }

        public static Dictionary<string, string> xmlBWNVPairsKeyValuePairs { get; set; }

        public static int counter { get; set; }

        public static XElement updatedNVPair { get; set; }

        public static XElement updatedBWNVPair { get; set; }

        public static XElement updatedNVPairsElement { get; set; }

        public static XElement updatedBWNVPairsElement { get; set; }

        public static XElement updatedBinding { get; set; }

        public static XElement updatedBWProcessElement { get; set; }

        public static IEnumerable<XElement> firstServiceNVPairs { get; set; }

        public static IEnumerable<XElement> secondServiceNVPairs { get; set; }

        public static IEnumerable<XElement> firstServiceBWNVPairs { get; set; }

        public static IEnumerable<XElement> secondServiceBWNVPairs { get; set; }

        public static IEnumerable<XElement> serviceBindings { get; set; }

        public static Dictionary<string, string> passwordDic { get; set; }

        public static Dictionary<string, string> environments { get; set; }

        public static KeyValuePair<string, string> environment { get; set; }

        public enum ENV { DEV, QA, PROD };

        public enum Extension { xml, properties };

        public static string primaryUrl { get; set; }

        public static string secondaryUrl { get; set; }

        public static string dotnetURL { get; set; }

        public static string databaseUrl { get; set; }

        public static string existingKeyName { get; set; }

        #endregion

        #region Constructor

        static Transformation()
        {
            nvPairs = "{http://www.tibco.com/xmlns/ApplicationManagement}NVPairs";
            gvName = "{http://www.tibco.com/xmlns/ApplicationManagement}name";
            gvValue = "{http://www.tibco.com/xmlns/ApplicationManagement}value";

            bw = "{http://www.tibco.com/xmlns/ApplicationManagement}bw";
            bindings = "{http://www.tibco.com/xmlns/ApplicationManagement}bindings";
            binding = "{http://www.tibco.com/xmlns/ApplicationManagement}binding";

            bindingType = "{http://www.tibco.com/xmlns/ApplicationManagement}type";
            bindingVersion = "{http://www.tibco.com/xmlns/ApplicationManagement}version";
            bindingLocation = "{http://www.tibco.com/xmlns/ApplicationManagement}location";
            bindingStartOnBoot = "{http://www.tibco.com/xmlns/ApplicationManagement}startOnBoot";
            bindingEnableVerbose = "{http://www.tibco.com/xmlns/ApplicationManagement}enableVerbose";
            bindingMaxLogFileSize = "{http://www.tibco.com/xmlns/ApplicationManagement}maxLogFileSize";
            bindingMaxLogFileCount = "{http://www.tibco.com/xmlns/ApplicationManagement}maxLogFileCount";
            bindingThreadCount = "{http://www.tibco.com/xmlns/ApplicationManagement}threadCount";
            bindingInitHeapSize = "{http://www.tibco.com/xmlns/ApplicationManagement}initHeapSize";
            bindingMaxHeapSize = "{http://www.tibco.com/xmlns/ApplicationManagement}maxHeapSize";
            bindingThreadStackSize = "{http://www.tibco.com/xmlns/ApplicationManagement}threadStackSize";
            bindingCheckpoint = "{http://www.tibco.com/xmlns/ApplicationManagement}checkpoint";
            bindingTimeOut = "{http://www.tibco.com/xmlns/ApplicationManagement}timeout";

            bwprocesses = "{http://www.tibco.com/xmlns/ApplicationManagement}bwprocesses";
            bwprocess = "{http://www.tibco.com/xmlns/ApplicationManagement}bwprocess";

            appendNVKeyText = "TIBCO-BW-";
            appendBindingKeyText = "SAAS-TIBCO-BW-";
            pipe = "||";
            pathSeperator = "\\";
            transform = ".transform";
            serviceFamilyNameComment = "Service Family: ";
            serviceNameComment = "Service: ";
            transformFileName = "transform-gvar.properties";

            firstServiceNVPairs = null;
            secondServiceNVPairs = null;
            serviceBindings = null;

            newKey = string.Empty;
            newGVValue = string.Empty;
            client = string.Empty;

            serviceFamilyAdded = false;
            serviceNameAdded = false;

            originalNVPairs = new Dictionary<string, string>();
            propertiesKeyValuePairs = new Dictionary<string, string>();
            xmlKeyValuePairs = new Dictionary<string, string>();
            xmlBWNVPairsKeyValuePairs = new Dictionary<string, string>();
            originalBinding = new Dictionary<string, string>();
            xmlBindingKeyValuePairs = new Dictionary<string, string>();
            passwordDic = new Dictionary<string, string>();
            environments = new Dictionary<string, string>();
            environment = new KeyValuePair<string, string>();

            primaryUrl = "PrimaryURL";
            secondaryUrl = "SecondaryURL";
            dotnetURL = "DotNetURL";
            databaseUrl = "DatabaseURL";
            existingKeyName = string.Empty;
        }

        #endregion

        #region Methods

        #region Compare Services

        public void Compare(string firstSourcePath, string secondSourcePath, string clientName, Dictionary<string, string> environments, string targetLocation)
        {
            firstPath = firstSourcePath;
            secondPath = secondSourcePath;
            client = clientName;
            transformationPath = targetLocation;

            foreach (var env in environments)
            {
                Initialize();
                environment = env;
                GetServiceFamilyName();

                transformedServiceFamilyPath = transformationPath + pathSeperator + environment.Key + pathSeperator + serviceFamilyName + transform;
                CheckFile();

                // File iteration logic
                string[] firstSourcePathServices = Directory.GetFiles(firstSourcePath, "*.xml", SearchOption.TopDirectoryOnly);
                string[] secondSourcePathServices = Directory.GetFiles(secondSourcePath, "*.xml", SearchOption.TopDirectoryOnly);

                //string[] secondSourcePathServices = Directory.GetFiles(secondSourcePath, "*.transform", SearchOption.TopDirectoryOnly);

                foreach (var serviceInFirstList in firstSourcePathServices)
                {
                    foreach (var serviceInSecondList in secondSourcePathServices)
                    {
                        if (Path.GetFileName(serviceInFirstList).Equals(Path.GetFileName(serviceInSecondList), StringComparison.InvariantCultureIgnoreCase))
                        //if (Path.GetFileNameWithoutExtension(serviceInFirstList).Equals(Path.GetFileNameWithoutExtension(serviceInSecondList), StringComparison.InvariantCultureIgnoreCase))
                        {
                            CompareService(serviceInFirstList, serviceInSecondList);
                        }
                    }
                }

                serviceFamilyAdded = false;
            }
        }

        private void Initialize()
        {
            newKey = string.Empty;
            newGVValue = string.Empty;

            serviceFamilyAdded = false;
            serviceNameAdded = false;

            firstServiceNVPairs = null;
            secondServiceNVPairs = null;
            serviceBindings = null;

            originalNVPairs = new Dictionary<string, string>();
            propertiesKeyValuePairs = new Dictionary<string, string>();
            xmlKeyValuePairs = new Dictionary<string, string>();
            originalBinding = new Dictionary<string, string>();
            xmlBindingKeyValuePairs = new Dictionary<string, string>();
            passwordDic = new Dictionary<string, string>();
            environments = new Dictionary<string, string>();
            environment = new KeyValuePair<string, string>();
        }

        private static void GetServiceFamilyName()
        {
            if (environment.Key.Equals(ENV.DEV.ToString(), StringComparison.InvariantCultureIgnoreCase) || environment.Key.Equals(ENV.PROD.ToString(), StringComparison.InvariantCultureIgnoreCase))
                serviceFamilyName = Path.GetFileName(firstPath);

            if (environment.Key.Equals(ENV.QA.ToString(), StringComparison.InvariantCultureIgnoreCase))
                serviceFamilyName = Path.GetFileName(secondPath);
        }

        private void CheckFile()
        {
            if (!string.IsNullOrEmpty(transformationPath) || !string.IsNullOrWhiteSpace(transformationPath))
            {
                if (Directory.Exists(transformationPath + pathSeperator + environment.Key))
                {
                    Directory.Delete(transformationPath + pathSeperator + environment.Key, true);
                }

                Directory.CreateDirectory(transformationPath + pathSeperator + environment.Key);

                if (!Directory.Exists(transformedServiceFamilyPath))
                    Directory.CreateDirectory(transformedServiceFamilyPath);

                if (environment.Key.Equals(ENV.DEV.ToString(), StringComparison.InvariantCultureIgnoreCase) || environment.Key.Equals(ENV.PROD.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!File.Exists(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value))
                        CreateFile((transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value), Extension.xml.ToString());
                }

                if (environment.Key.Equals(ENV.QA.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!File.Exists(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value))
                        CreateFile((transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value), Extension.xml.ToString());
                }

                if (!File.Exists(transformationPath + pathSeperator + environment.Key + pathSeperator + transformFileName))
                    CreateFile((transformationPath + pathSeperator + environment.Key + pathSeperator + transformFileName), Extension.properties.ToString());
            }
            else
                throw new ArgumentException();
        }

        private void CompareService(string firstServicePath, string secondServicePath)
        {
            if (environment.Key.Equals(ENV.DEV.ToString(), StringComparison.InvariantCultureIgnoreCase) || environment.Key.Equals(ENV.PROD.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                serviceName = Path.GetFileName(firstServicePath);
                CreateApplicationConfiguration(XDocument.Load(firstServicePath), XDocument.Load(secondServicePath), serviceName);
            }

            if (environment.Key.Equals(ENV.QA.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                serviceName = Path.GetFileName(secondServicePath);
                CreateApplicationConfiguration(XDocument.Load(secondServicePath), XDocument.Load(firstServicePath), serviceName);
            }

        }

        private void CreateApplicationConfiguration(XDocument firstService, XDocument secondService, string serviceName)
        {
            GetGlobalVariables(firstService, secondService, serviceName);

            if ((firstService.Root.Descendants(bw).FirstOrDefault() != null && secondService.Root.Descendants(bw).FirstOrDefault() != null) && !serviceName.Contains("ADB-"))
            {
                GetBWGlobalVariables(firstService, secondService, serviceName);

                GetBindings(firstService, serviceName); 
            }

            //GetBWProcessElements(firstService, secondService, serviceName);
        }

        //private void GetBWProcessElements(XDocument firstService, XDocument secondService, string serviceName)
        //{
        //    //BW Processes
        //    IEnumerable<XElement> firstServiceBWProcess = firstService.Root.Descendants(bwprocesses).Elements();
        //    IEnumerable<XElement> secondServiceBWProcess = secondService.Root.Descendants(bwprocesses).Elements();

        //    xmlKeyValuePairs.Clear();
        //    propertiesKeyValuePairs.Clear();

        //    updatedBWProcessElement = new XElement(firstService.Root.Element(bwprocesses));
        //    updatedBWProcessElement.RemoveNodes();

        //    updatedBWProcessElement = CompareBWProcess();

        //    AddKeysToFile();

        //    firstService.Root.Element(bwprocesses).ReplaceWith(updatedBWProcessElement);
        //    firstService.Save(transformedServiceFamilyPath + pathSeperator + Path.GetFileNameWithoutExtension(serviceName) + transform);
        //}

        //private XElement CompareBWProcess()
        //{
        //    return null;
        //}

        #endregion

        #region Global Variables Comparision

        private void GetGlobalVariables(XDocument firstService, XDocument secondService, string serviceName)
        {
            //Global Variables
            firstServiceNVPairs = firstService.Root.Element(nvPairs).Elements();
            secondServiceNVPairs = secondService.Root.Element(nvPairs).Elements();

            xmlKeyValuePairs.Clear();
            propertiesKeyValuePairs.Clear();

            updatedNVPairsElement = new XElement(firstService.Root.Element(nvPairs));
            updatedNVPairsElement.RemoveNodes();

            updatedNVPairsElement = CompareNVPairs();

            AddKeysToFile();

            firstService.Root.Element(nvPairs).ReplaceWith(updatedNVPairsElement);
            firstService.Save(transformedServiceFamilyPath + pathSeperator + Path.GetFileNameWithoutExtension(serviceName) + transform);
        }

        private XElement CompareNVPairs()
        {
            Hashtable nvPairHashTable = new Hashtable();
            passwordDic = new Dictionary<string, string>();

            foreach (var nvPair in secondServiceNVPairs)
            {
                nvPairHashTable.Add(nvPair.Descendants(gvName).FirstOrDefault().Value, nvPair.Descendants(gvValue).FirstOrDefault().Value);
            }

            foreach (var nvPair in firstServiceNVPairs)
            {
                string key = nvPair.Element(gvName).Value;
                string value = nvPair.Element(gvValue).Value;

                GetServicePasswordKeys(nvPair);

                updatedNVPair = CreateNVPairElement(nvPair);

                if ((nvPairHashTable.ContainsKey(key) && !nvPairHashTable[key].Equals(value)) || CheckKeyContainsUsername(key.Split('/').Last())
                    || CheckKeyContainsAnyCertificateURL(key))
                {
                    if (!CheckKeyContainsStaticFields(key.Split('/').Last()))
                    {
                        newKey = (appendNVKeyText + key).Replace("/", "-");
                        newGVValue = (pipe + newKey + pipe);

                        CheckNameValueKey(key, value, newKey, nvPair);
                    }
                    else
                    {
                        updatedNVPairsElement.Add(nvPair);
                    }
                }
                else
                {
                    updatedNVPairsElement.Add(nvPair);
                }
            }

            serviceNameAdded = false;

            return updatedNVPairsElement;
        }

        private static XElement CreateNVPairElement(XElement nvPair)
        {
            return new XElement(nvPair.Name, new XElement(nvPair.Descendants(gvName).FirstOrDefault().Name), new XElement(nvPair.Descendants(gvValue).FirstOrDefault().Name));
        }

        private static void GetServicePasswordKeys(XElement nvPair)
        {
            if (nvPair.Descendants(gvName).FirstOrDefault().Value.Contains("Password") || nvPair.Descendants(gvName).FirstOrDefault().Value.Contains("password") || nvPair.Descendants(gvName).FirstOrDefault().Value.Contains("PASSWORD"))
            {
                passwordDic.Add(nvPair.Descendants(gvName).FirstOrDefault().Value, nvPair.Descendants(gvValue).FirstOrDefault().Value);
            }
        }

        private static bool CheckKeyContainsUsername(string key)
        {
            return (key.ToUpper().Contains("USERNAME") || key.Contains("username") || key.Contains("UserName") || key.Contains("userName") ||
                    key.Contains("USERNAME") || key.Contains("User") || key.Contains("user") || key.Contains("User") || key.Contains("USER"));
        }

        private static bool CheckKeyContainsAnyCertificateURL(string key)
        {
            return (key.ToUpper().Contains("SSLCERTURL") || key.ToLower().Contains("sslcerturl") || key.Contains("SSLCertURL") ||
                    key.Contains("SSLCertificateURL") || key.ToLower().Contains("sslcertificateurl") || key.ToUpper().Contains("SSLCERTIFICATEURL") ||
                    key.ToUpper().Contains("IDENTITYURL") || key.ToLower().Contains("identityurl") || key.Contains("IdentityURL") ||
                    key.ToUpper().Contains("BW_GLOBAL_TRUSTED_CA_STORE"));
        }

        private static bool CheckKeyContainsStaticFields(string key)
        {
            return (key.ToUpper().Contains("SERVICENAME") || key.ToUpper().Contains("CLIENTID") ||
                    key.ToUpper().Contains("SERVICE NAME") || key.ToUpper().Contains("CLIENT ID") ||
                    key.ToUpper().Contains("SERVICE_NAME") || key.ToUpper().Contains("CLIENT_ID") ||
                    key.ToUpper().Contains("ROUTINGKEY") || key.ToUpper().Contains("MESSAGESELECTOR") ||
                    key.ToUpper().Contains("ROUTING KEY") || key.ToUpper().Contains("MESSAGE SELECTOR") ||
                    key.ToUpper().Contains("ROUTING_KEY") || key.ToUpper().Contains("MESSAGE_SELECTOR"));
        }

        private bool CheckURLAlreadyExists(string gvKey, string gvValue)
        {
            if (CheckForURLInKey(gvKey))
            {
                string url = gvKey.Split('/').Last().ToUpper();

                foreach (var nvPair in originalNVPairs)
                {
                    if (nvPair.Key.Contains(url))
                    {
                        if (nvPair.Key.Contains(url) && nvPair.Value.ToUpper().Equals(gvValue.ToUpper(), StringComparison.InvariantCultureIgnoreCase))
                        {
                            XDocument pdfFile = XDocument.Load(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value);
                            IEnumerable<XElement> pdfkeys = pdfFile.Root.Descendants().FirstOrDefault().Elements();

                            foreach (var key in pdfkeys)
                            {
                                string keyValue = key.Attribute("key").Value;
                                string val = key.Value;

                                if (keyValue.ToLower().Contains(url.ToLower()) && val.Equals(gvValue, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    existingKeyName = key.Attribute("key").Value;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        private bool CheckForURLInKey(string key)
        {
            return key.ToUpper().Contains(primaryUrl.ToUpper()) || key.ToUpper().Contains(secondaryUrl.ToUpper()) ||
                   key.ToUpper().Contains(dotnetURL.ToUpper()) || key.ToUpper().Contains(databaseUrl.ToUpper());
        }

        private void CheckNameValueKey(string oldKey, string value, string newFormedKey, XElement nvPair)
        {
            if (!CheckURLAlreadyExists(oldKey, value))
            {
                if ((!originalNVPairs.Any() || !originalNVPairs.Keys.Contains(oldKey, StringComparer.InvariantCultureIgnoreCase)) &&
                    !CheckKeyContainsUsername(oldKey.Split('/').Last()) && !HasPassword(oldKey.Split('/').Last()))
                {
                    KeyValueNotPresentInOriginalDictionary(oldKey, value, newFormedKey, nvPair);
                }

                else if (originalNVPairs.Values.Contains(value, StringComparer.InvariantCultureIgnoreCase) &&
                         !CheckKeyContainsUsername(oldKey.Split('/').Last()) && !HasPassword(oldKey.Split('/').Last()))
                {
                    KeyValuePresentInOriginalDictionary(value, nvPair);
                }

                else if ((!originalNVPairs.Keys.Contains(oldKey, StringComparer.InvariantCultureIgnoreCase) ||
                         originalNVPairs.Keys.Contains(oldKey, StringComparer.InvariantCultureIgnoreCase) ||
                         !originalNVPairs.Values.Contains(value, StringComparer.InvariantCultureIgnoreCase) ||
                         originalNVPairs.Values.Contains(value, StringComparer.InvariantCultureIgnoreCase)) &&
                         (CheckKeyContainsUsername(oldKey.Split('/').Last()) && !HasPassword(oldKey.Split('/').Last())))
                {
                    counter = 0;
                    if (originalNVPairs.Values.Contains(value, StringComparer.InvariantCultureIgnoreCase))
                    {
                        string key = GetRelativeKey(value);
                        nvPair.Element(gvValue).Value = pipe + key + pipe;
                        updatedNVPairsElement.Add(nvPair);
                        //GetRelativePassword(key, oldKey);
                    }
                    else
                    {
                        counter = originalNVPairs.Count(x => x.Key.Contains(oldKey) || x.Key.Equals(oldKey, StringComparison.InvariantCultureIgnoreCase));

                        if (counter > 0)
                        {
                            newFormedKey = newFormedKey + "_" + counter.ToString();
                            originalNVPairs.Add(oldKey + "_" + counter.ToString(), value);
                            propertiesKeyValuePairs.Add(oldKey, newFormedKey);
                            xmlKeyValuePairs.Add(newFormedKey, value);
                        }
                        else if (!originalNVPairs.Values.Contains(value, StringComparer.InvariantCultureIgnoreCase))
                        {
                            originalNVPairs.Add(oldKey, value);
                            propertiesKeyValuePairs.Add(oldKey, newFormedKey);
                            xmlKeyValuePairs.Add(newFormedKey, value);
                        }

                        nvPair.Element(gvValue).Value = pipe + newFormedKey + pipe;

                        updatedNVPairsElement.Add(nvPair);

                        //GetPasswordForUser(oldKey);
                    }
                }

                else if (originalNVPairs.Keys.Contains(oldKey, StringComparer.InvariantCultureIgnoreCase) &&
                         !originalNVPairs.Values.Contains(value, StringComparer.InvariantCultureIgnoreCase) &&
                         !HasPassword(oldKey.Split('/').Last()))
                {
                    counter = 0;
                    counter = originalNVPairs.Count(x => x.Key.Contains(oldKey));

                    if (counter > 0)
                    {
                        newFormedKey = newFormedKey + "_" + counter.ToString();
                        originalNVPairs.Add(oldKey + "_" + counter.ToString(), value);
                        propertiesKeyValuePairs.Add(oldKey, newFormedKey);
                        xmlKeyValuePairs.Add(newFormedKey, value);
                    }

                    nvPair.Element(gvValue).Value = pipe + newFormedKey + pipe;

                    updatedNVPairsElement.Add(nvPair);
                }
                else if (HasPassword(oldKey.Split('/').Last()) && passwordDic.Keys.Contains(oldKey, StringComparer.InvariantCultureIgnoreCase))
                {
                    counter = 0;
                    string keyName = oldKey;
                    //counter = originalNVPairs.Count(x => x.Key.Contains(oldKey));
                    counter = originalNVPairs.Keys.Where(x => x.ToUpper().Contains(keyName.ToUpper())).Count();

                    if (counter > 0)
                    {
                        originalNVPairs.Add(oldKey + "_" + counter.ToString(), value);
                        propertiesKeyValuePairs.Add(oldKey, (appendNVKeyText + oldKey).Replace("/", "-") + "_" + counter.ToString());
                        xmlKeyValuePairs.Add((appendNVKeyText + oldKey).Replace("/", "-") + "_" + counter.ToString(), value);
                        nvPair.Element(gvValue).Value = pipe + (appendNVKeyText + oldKey).Replace("/", "-") + "_" + counter.ToString() + pipe;
                        updatedNVPairsElement.Add(nvPair);
                        passwordDic.Remove(oldKey);
                    }
                    else
                    {
                        originalNVPairs.Add(oldKey, value);
                        propertiesKeyValuePairs.Add(oldKey, (appendNVKeyText + oldKey).Replace("/", "-"));
                        xmlKeyValuePairs.Add((appendNVKeyText + oldKey).Replace("/", "-"), value);
                        nvPair.Element(gvValue).Value = pipe + (appendNVKeyText + oldKey).Replace("/", "-") + pipe;
                        updatedNVPairsElement.Add(nvPair);
                        passwordDic.Remove(oldKey);
                    }
                }
            }
            else
            {
                if (!HasPassword(oldKey.Split('/').Last()))
                {
                    originalNVPairs.Add(oldKey, newFormedKey);
                    propertiesKeyValuePairs.Add(oldKey, newFormedKey);
                    xmlKeyValuePairs.Add(newFormedKey, value);
                    nvPair.Element(gvValue).Value = pipe + existingKeyName + pipe;
                    updatedNVPairsElement.Add(nvPair);
                }
            }
        }

        private static void KeyValueNotPresentInOriginalDictionary(string oldKey, string value, string newFormedKey, XElement nvPair)
        {
            if (!originalNVPairs.Values.Contains(value, StringComparer.InvariantCultureIgnoreCase) && !HasPassword(oldKey.Split('/').Last()))
            {
                originalNVPairs.Add(oldKey, value);
                propertiesKeyValuePairs.Add(oldKey, newFormedKey);
                xmlKeyValuePairs.Add(newFormedKey, value);

                nvPair.Element(gvValue).Value = pipe + newFormedKey + pipe;
                updatedNVPairsElement.Add(nvPair);
            }
            else if (originalNVPairs.Values.Contains(value, StringComparer.InvariantCultureIgnoreCase) && !HasPassword(oldKey.Split('/').Last()))
            {
                string key = GetRelativeKey(value);

                nvPair.Element(gvValue).Value = pipe + key + pipe;

                updatedNVPairsElement.Add(nvPair);
            }
        }

        private static void KeyValuePresentInOriginalDictionary(string value, XElement nvPair)
        {
            string key = GetRelativeKey(value);

            nvPair.Element(gvValue).Value = pipe + key + pipe;

            updatedNVPairsElement.Add(nvPair);
        }

        private static string GetRelativeKey(string value)
        {
            string key = string.Empty;
            XDocument pdfFile = XDocument.Load(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value);
            IEnumerable<XElement> pdfKeys = pdfFile.Root.Descendants().FirstOrDefault().Elements();

            if (pdfKeys.Any() && pdfKeys.Where(x => x.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase)).Any())
            {
                key = pdfKeys.First(x => x.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase)).Attribute("key").Value;
            }
            else if (originalNVPairs.Values.Contains(value, StringComparer.InvariantCultureIgnoreCase))
            {
                key = originalNVPairs.First(x => x.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase)).Key;
                key = appendNVKeyText + key.Replace("/", "-");
            }
            return key;
        }

        private static bool HasPassword(string keyToCheck)
        {
            return keyToCheck.Contains("Password") || keyToCheck.Contains("PassWord") || keyToCheck.Contains("PASSWORD") || keyToCheck.Contains("password");
        }

        private XElement GetCorrespondingPasswordGV(string passwordKey)
        {
            foreach (var nvPair in firstServiceNVPairs)
            {
                string key = nvPair.Descendants(gvName).FirstOrDefault().Value;
                if (key.Equals(passwordKey, StringComparison.InvariantCultureIgnoreCase))
                {
                    return nvPair;
                }
            }
            return null;
        }

        private void AddKeysToFile()
        {
            if (xmlKeyValuePairs.Any())
                AddNVPairToPdfFile();

            if (propertiesKeyValuePairs.Any())
                AddKeyToPropertiesFile();
        }

        private void AddKeyToPropertiesFile()
        {
            //#Start# Properties File
            foreach (var keyValuePair in propertiesKeyValuePairs)
            {
                StringBuilder oldProperties = new StringBuilder(File.ReadAllText(transformationPath + pathSeperator + environment.Key + pathSeperator + transformFileName));
                File.WriteAllText(transformationPath + pathSeperator + environment.Key + pathSeperator + transformFileName, oldProperties + Environment.NewLine + keyValuePair.Key + "=" + keyValuePair.Value);
            }
            //#End#
        }

        private void AddNVPairToPdfFile()
        {
            AddComment();

            if (environment.Key.Equals(ENV.DEV.ToString(), StringComparison.InvariantCultureIgnoreCase) || environment.Key.Equals(ENV.PROD.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                //#Start# XML File
                XDocument xmlFile = XDocument.Load(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value);

                foreach (var keyValuePair in xmlKeyValuePairs)
                {
                    XElement addKey = new XElement("KeyValuePair", new XAttribute("key", keyValuePair.Key), keyValuePair.Value);
                    xmlFile.Root.Descendants().FirstOrDefault().Add(addKey);
                }
                xmlFile.Save(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value);
                //#End# 
            }


            if (environment.Key.Equals(ENV.QA.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                //#Start# XML File
                XDocument xmlFile = XDocument.Load(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value);

                foreach (var keyValuePair in xmlKeyValuePairs)
                {
                    XElement addKey = new XElement("KeyValuePair", new XAttribute("key", keyValuePair.Key), keyValuePair.Value);
                    xmlFile.Root.Descendants().FirstOrDefault().Add(addKey);
                }
                xmlFile.Save(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value);
                //#End# 
            }
        }

        #endregion

        #region Binding Comparison

        private void GetBindings(XDocument firstService, string serviceName)
        {
            //Bindings
            serviceBindings = firstService.Root.Descendants(bindings).Elements();

            xmlBindingKeyValuePairs.Clear();

            GetBindingElements();

            AddBindingKeysToFile();
        }

        private void GetBindingElements()
        {
            foreach (var binding in serviceBindings)
            {
                string key = binding.Attribute("name").Value;
                string typeValue = binding.Descendants(bindingType).FirstOrDefault().Value;

                newKey = (appendBindingKeyText + key.Split('-')[0]);

                CheckBindingKey(typeValue, newKey, binding);
            }
        }

        private void CheckBindingKey(string typeVvalue, string newKey, XElement bindingElement)
        {
            if (!originalBinding.Any() || !originalBinding.ContainsKey(newKey + "-Type"))
            {
                originalBinding.Add(newKey + "-Type", typeVvalue);

                xmlBindingKeyValuePairs = new Dictionary<string, string>()
                    {
                        { newKey + "-Type", typeVvalue },
                        { newKey + "-Version", bindingElement.Descendants(bindingVersion).FirstOrDefault().Value },
                        { newKey + "-Location", bindingElement.Descendants(bindingLocation).FirstOrDefault().Value },
                        { newKey + "-StartOnBoot", bindingElement.Descendants(bindingStartOnBoot).FirstOrDefault().Value },
                        { newKey + "-EnableVerbose", bindingElement.Descendants(bindingEnableVerbose).FirstOrDefault().Value },
                        { newKey + "-MaxLogFileSize", bindingElement.Descendants(bindingMaxLogFileSize).FirstOrDefault().Value },
                        { newKey + "-MaxLogFileCount", bindingElement.Descendants(bindingMaxLogFileCount).FirstOrDefault().Value },
                        { newKey + "-ThreadCount", bindingElement.Descendants(bindingThreadCount).FirstOrDefault().Value },
                        { newKey + "-InitHeapSize", bindingElement.Descendants(bindingInitHeapSize).FirstOrDefault().Value },
                        { newKey + "-MaxHeapSize", bindingElement.Descendants(bindingMaxHeapSize).FirstOrDefault().Value },
                        { newKey + "-ThreadStackSize", bindingElement.Descendants(bindingThreadStackSize).FirstOrDefault().Value },
                        { newKey + "-Checkpoint", bindingElement.Descendants(bindingCheckpoint).FirstOrDefault().Value },
                        { newKey + "-TimeOut", bindingElement.Descendants(bindingTimeOut).FirstOrDefault().Value }
                    };
            }
        }

        private void AddBindingKeysToFile()
        {
            if (xmlBindingKeyValuePairs.Any())
            {
                AddCommentToXMLFile(string.Empty, "Binding ");
                AddBindingToPdfFile();
            }
        }

        private void AddBindingToPdfFile()
        {
            if (environment.Key.Equals(ENV.DEV.ToString(), StringComparison.InvariantCultureIgnoreCase) || environment.Key.Equals(ENV.PROD.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                //#Start# XML File
                XDocument xmlFile = XDocument.Load(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value);

                foreach (var keyValuePair in xmlBindingKeyValuePairs)
                {
                    XElement addKey = new XElement("KeyValuePair", new XAttribute("key", keyValuePair.Key), keyValuePair.Value);
                    xmlFile.Root.Descendants().FirstOrDefault().Add(addKey);
                }
                xmlFile.Add(Environment.NewLine);
                xmlFile.Save(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value);
                //#End# 
            }

            if (environment.Key.Equals(ENV.QA.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                //#Start# XML File
                XDocument xmlFile = XDocument.Load(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value);

                foreach (var keyValuePair in xmlBindingKeyValuePairs)
                {
                    XElement addKey = new XElement("KeyValuePair", new XAttribute("key", keyValuePair.Key), keyValuePair.Value);
                    xmlFile.Root.Descendants().FirstOrDefault().Add(addKey);
                }
                xmlFile.Add(Environment.NewLine);
                xmlFile.Save(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value);
                //#End# 
            }
        }

        #endregion

        #region BW Global Variables

        private void GetBWGlobalVariables(XDocument firstService, XDocument secondService, string serviceName)
        {
            //Global Variables
            firstServiceBWNVPairs = firstService.Root.Descendants(bw).FirstOrDefault().Elements(nvPairs);
            secondServiceBWNVPairs = secondService.Root.Descendants(bw).FirstOrDefault().Elements(nvPairs);

            xmlBWNVPairsKeyValuePairs.Clear();
            xmlKeyValuePairs.Clear();
            propertiesKeyValuePairs.Clear();

            foreach (var NVPair1 in firstServiceBWNVPairs)
            {
                foreach (var NVPair2 in secondServiceBWNVPairs)
                {
                    if (NVPair1.HasAttributes && NVPair2.HasAttributes && NVPair1.Attribute("name").Value.Equals(NVPair2.Attribute("name").Value, StringComparison.InvariantCultureIgnoreCase))
                    {
                        updatedBWNVPairsElement = new XElement(NVPair1);
                        updatedBWNVPairsElement.RemoveNodes();

                        updatedBWNVPairsElement = CompareNVPairs(NVPair1.Elements(), NVPair2.Elements());

                        AddBWKeysToFile();

                        firstService.Root.Descendants(bw).FirstOrDefault().Element(nvPairs).ReplaceWith(updatedBWNVPairsElement);
                        firstService.Save(transformedServiceFamilyPath + pathSeperator + Path.GetFileNameWithoutExtension(serviceName) + transform);
                    }
                }
            }
        }

        private XElement CompareNVPairs(IEnumerable<XElement> firstServiceBWNVPairs, IEnumerable<XElement> secondServiceBWNVPairs)
        {
            Hashtable nvPairHashTable = new Hashtable();
            passwordDic.Clear();

            foreach (var nvPair in secondServiceBWNVPairs)
            {
                nvPairHashTable.Add(nvPair.Descendants(gvName).FirstOrDefault().Value, nvPair.Descendants(gvValue).FirstOrDefault().Value);
            }

            foreach (var nvPair in firstServiceBWNVPairs)
            {
                string key = nvPair.Element(gvName).Value;
                string value = nvPair.Element(gvValue).Value;

                GetServicePasswordKeys(nvPair);

                updatedBWNVPair = CreateNVPairElement(nvPair);

                if ((nvPairHashTable.ContainsKey(key) && !nvPairHashTable[key].Equals(value)) || CheckKeyContainsUsername(key.Split('/').Last()))
                {
                    if (!CheckKeyContainsStaticFields(key.Split('/').Last()))
                    {
                        newKey = (appendNVKeyText + key).Replace("/", "-");
                        newGVValue = (pipe + newKey + pipe);

                        CheckBWNameValueKey(key, value, newKey, nvPair);
                    }
                    else
                    {
                        updatedBWNVPairsElement.Add(nvPair);
                    }
                }
                else
                {
                    updatedBWNVPairsElement.Add(nvPair);
                }
            }

            return updatedBWNVPairsElement;
        }

        private void CheckBWNameValueKey(string oldKey, string value, string newFormedKey, XElement nvPair)
        {
            if (!CheckURLAlreadyExists(oldKey, value))
            {
                if ((!originalNVPairs.Any() || !originalNVPairs.Keys.Contains(oldKey, StringComparer.InvariantCultureIgnoreCase)) &&
                    !CheckKeyContainsUsername(oldKey.Split('/').Last()) && !HasPassword(oldKey.Split('/').Last()))
                {
                    BWKeyValueNotPresentInOriginalDictionary(oldKey, value, newFormedKey, nvPair);
                }

                else if (originalNVPairs.Values.Contains(value, StringComparer.InvariantCultureIgnoreCase) &&
                         !CheckKeyContainsUsername(oldKey.Split('/').Last()) && !HasPassword(oldKey.Split('/').Last()))
                {
                    BWKeyValuePresentInOriginalDictionary(value, nvPair);
                }

                else if ((!originalNVPairs.Keys.Contains(oldKey, StringComparer.InvariantCultureIgnoreCase) ||
                         originalNVPairs.Keys.Contains(oldKey, StringComparer.InvariantCultureIgnoreCase) ||
                         !originalNVPairs.Values.Contains(value, StringComparer.InvariantCultureIgnoreCase) ||
                         originalNVPairs.Values.Contains(value, StringComparer.InvariantCultureIgnoreCase)) &&
                         (CheckKeyContainsUsername(oldKey.Split('/').Last()) && !HasPassword(oldKey.Split('/').Last())))
                {
                    counter = 0;
                    if (originalNVPairs.Values.Contains(value, StringComparer.InvariantCultureIgnoreCase))
                    {
                        string key = GetRelativeKey(value);
                        nvPair.Element(gvValue).Value = pipe + key + pipe;
                        updatedBWNVPairsElement.Add(nvPair);
                        //GetRelativePassword(key, oldKey);
                    }
                    else
                    {
                        counter = originalNVPairs.Count(x => x.Key.Contains(oldKey) || x.Key.Equals(oldKey, StringComparison.InvariantCultureIgnoreCase));

                        if (counter > 0)
                        {
                            newFormedKey = newFormedKey + "_" + counter.ToString();
                            originalNVPairs.Add(oldKey + "_" + counter.ToString(), value);
                            propertiesKeyValuePairs.Add(oldKey, newFormedKey);
                            xmlBWNVPairsKeyValuePairs.Add(newFormedKey, value);
                        }
                        else if (!originalNVPairs.Values.Contains(value, StringComparer.InvariantCultureIgnoreCase))
                        {
                            originalNVPairs.Add(oldKey, value);
                            propertiesKeyValuePairs.Add(oldKey, newFormedKey);
                            xmlBWNVPairsKeyValuePairs.Add(newFormedKey, value);
                        }

                        nvPair.Element(gvValue).Value = pipe + newFormedKey + pipe;

                        updatedBWNVPairsElement.Add(nvPair);

                        //GetPasswordForUser(oldKey);
                    }
                }

                else if (originalNVPairs.Keys.Contains(oldKey, StringComparer.InvariantCultureIgnoreCase) &&
                         !originalNVPairs.Values.Contains(value, StringComparer.InvariantCultureIgnoreCase) &&
                         !HasPassword(oldKey.Split('/').Last()))
                {
                    counter = 0;
                    counter = originalNVPairs.Count(x => x.Key.Contains(oldKey));

                    if (counter > 0)
                    {
                        newFormedKey = newFormedKey + "_" + counter.ToString();
                        originalNVPairs.Add(oldKey + "_" + counter.ToString(), value);
                        propertiesKeyValuePairs.Add(oldKey, newFormedKey);
                        xmlBWNVPairsKeyValuePairs.Add(newFormedKey, value);
                    }

                    nvPair.Element(gvValue).Value = pipe + newFormedKey + pipe;

                    updatedBWNVPairsElement.Add(nvPair);
                }
                else if (HasPassword(oldKey.Split('/').Last()) && passwordDic.Keys.Contains(oldKey, StringComparer.InvariantCultureIgnoreCase))
                {
                    counter = 0;
                    string keyName = oldKey;
                    counter = originalNVPairs.Keys.Where(x => x.ToUpper().Contains(keyName.ToUpper())).Count();

                    if (counter > 0)
                    {
                        originalNVPairs.Add(oldKey + "_" + counter.ToString(), value);
                        propertiesKeyValuePairs.Add(oldKey, (appendNVKeyText + oldKey).Replace("/", "-") + "_" + counter.ToString());
                        xmlBWNVPairsKeyValuePairs.Add((appendNVKeyText + oldKey).Replace("/", "-") + "_" + counter.ToString(), value);
                        nvPair.Element(gvValue).Value = pipe + (appendNVKeyText + oldKey).Replace("/", "-") + "_" + counter.ToString() + pipe;
                        updatedBWNVPairsElement.Add(nvPair);
                        passwordDic.Remove(oldKey);
                    }
                    else
                    {
                        originalNVPairs.Add(oldKey, value);
                        propertiesKeyValuePairs.Add(oldKey, (appendNVKeyText + oldKey).Replace("/", "-"));
                        xmlBWNVPairsKeyValuePairs.Add((appendNVKeyText + oldKey).Replace("/", "-"), value);
                        nvPair.Element(gvValue).Value = pipe + (appendNVKeyText + oldKey).Replace("/", "-") + pipe;
                        updatedBWNVPairsElement.Add(nvPair);
                        passwordDic.Remove(oldKey);
                    }
                }
            }
            else
            {
                if (!HasPassword(oldKey.Split('/').Last()))
                {
                    originalNVPairs.Add(oldKey, newFormedKey);
                    propertiesKeyValuePairs.Add(oldKey, newFormedKey);
                    xmlBWNVPairsKeyValuePairs.Add(newFormedKey, value);
                    nvPair.Element(gvValue).Value = pipe + existingKeyName + pipe;
                    updatedBWNVPairsElement.Add(nvPair);
                }
            }
        }

        private static void BWKeyValueNotPresentInOriginalDictionary(string oldKey, string value, string newFormedKey, XElement nvPair)
        {
            if (!originalNVPairs.Values.Contains(value, StringComparer.InvariantCultureIgnoreCase) && !HasPassword(oldKey.Split('/').Last()))
            {
                originalNVPairs.Add(oldKey, value);
                propertiesKeyValuePairs.Add(oldKey, newFormedKey);
                xmlBWNVPairsKeyValuePairs.Add(newFormedKey, value);

                nvPair.Element(gvValue).Value = pipe + newFormedKey + pipe;
                updatedBWNVPairsElement.Add(nvPair);
            }
            else if (originalNVPairs.Values.Contains(value, StringComparer.InvariantCultureIgnoreCase) && !HasPassword(oldKey.Split('/').Last()))
            {
                string key = GetRelativeKey(value);

                nvPair.Element(gvValue).Value = pipe + key + pipe;

                updatedBWNVPairsElement.Add(nvPair);
            }
        }

        private static void BWKeyValuePresentInOriginalDictionary(string value, XElement nvPair)
        {
            string key = GetRelativeKey(value);

            nvPair.Element(gvValue).Value = pipe + key + pipe;

            updatedBWNVPairsElement.Add(nvPair);
        }

        private void AddBWKeysToFile()
        {
            if (xmlBWNVPairsKeyValuePairs.Any())
                AddBWNVPairToPdfFile();

            if (propertiesKeyValuePairs.Any())
                AddBWKeyToPropertiesFile();
        }

        private void AddBWKeyToPropertiesFile()
        {
            //#Start# Properties File
            foreach (var keyValuePair in propertiesKeyValuePairs)
            {
                StringBuilder oldProperties = new StringBuilder(File.ReadAllText(transformationPath + pathSeperator + environment.Key + pathSeperator + transformFileName));
                File.WriteAllText(transformationPath + pathSeperator + environment.Key + pathSeperator + transformFileName, oldProperties + Environment.NewLine + keyValuePair.Key + "=" + keyValuePair.Value);
            }
            //#End#
        }

        private void AddBWNVPairToPdfFile()
        {
            AddComment();

            if (environment.Key.Equals(ENV.DEV.ToString(), StringComparison.InvariantCultureIgnoreCase) || environment.Key.Equals(ENV.PROD.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                //#Start# XML File
                XDocument xmlFile = XDocument.Load(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value);

                foreach (var keyValuePair in xmlBWNVPairsKeyValuePairs)
                {
                    XElement addKey = new XElement("KeyValuePair", new XAttribute("key", keyValuePair.Key), keyValuePair.Value);
                    xmlFile.Root.Descendants().FirstOrDefault().Add(addKey);
                }
                xmlFile.Save(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value);
                //#End# 
            }


            if (environment.Key.Equals(ENV.QA.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                //#Start# XML File
                XDocument xmlFile = XDocument.Load(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value);

                foreach (var keyValuePair in xmlBWNVPairsKeyValuePairs)
                {
                    XElement addKey = new XElement("KeyValuePair", new XAttribute("key", keyValuePair.Key), keyValuePair.Value);
                    xmlFile.Root.Descendants().FirstOrDefault().Add(addKey);
                }
                xmlFile.Save(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value);
                //#End# 
            }
        }

        #endregion

        #region Create PDF & Transform Files

        private void CreateFile(string filePath, string extension)
        {
            if (extension.Equals(Extension.properties.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                var propertyFile = File.Create(filePath);
                propertyFile.Close();
            }

            if (extension.Equals(Extension.xml.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                XDocument pdf_DEVInt = new XDocument();
                pdf_DEVInt.Add(new XElement("platform", new XAttribute("environment", environment.Key), new XAttribute("client", client)));
                pdf_DEVInt.Root.Add(new XElement("KeyValuePairs"));
                pdf_DEVInt.Save(filePath);
            }
        }

        private void AddComment()
        {
            //Add Service Family Name Comment in the XML File
            if (!serviceFamilyAdded)
                AddCommentToXMLFile(serviceFamilyName, serviceFamilyNameComment);

            //Add Service Name Comment in the XML File
            if (!serviceNameAdded)
                AddCommentToXMLFile(serviceName, serviceNameComment);
        }

        private void AddCommentToXMLFile(string comment, string appendText)
        {
            if (environment.Key.Equals(ENV.DEV.ToString(), StringComparison.InvariantCultureIgnoreCase) || environment.Key.Equals(ENV.PROD.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                XDocument xmlFile = XDocument.Load(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value);
                xmlFile.Root.Descendants().FirstOrDefault().Add(new XComment(appendText + comment));
                xmlFile.Save(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value);

                if (appendText.Equals(serviceFamilyNameComment, StringComparison.InvariantCultureIgnoreCase))
                    serviceFamilyAdded = true;

                if (appendText.Equals(serviceNameComment, StringComparison.InvariantCultureIgnoreCase))
                    serviceNameAdded = true;
            }

            if (environment.Key.Equals(ENV.QA.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                XDocument xmlFile = XDocument.Load(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value);
                xmlFile.Root.Descendants().FirstOrDefault().Add(new XComment(appendText + comment));
                xmlFile.Save(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value);

                if (appendText.Equals(serviceFamilyNameComment, StringComparison.InvariantCultureIgnoreCase))
                    serviceFamilyAdded = true;

                if (appendText.Equals(serviceNameComment, StringComparison.InvariantCultureIgnoreCase))
                    serviceNameAdded = true;
            }
        }

        #endregion

        #endregion

        #region Unused Code for Handling Passwords
        //private void GetRelativePassword(string usernameKey, string oldKey)
        //{
        //    string passwordExistingKey = Path.GetDirectoryName(oldKey).Replace("\\", "/") + "/Password";
        //    string passwordKey = Path.GetDirectoryName(usernameKey.Replace("-", "\\"));
        //    string passwordLastKey = usernameKey.Replace("-", "\\").Split('\\').Last();
        //    if (passwordLastKey.Contains("_"))
        //    {
        //        passwordKey = (passwordKey + pathSeperator + passwordLastKey.Split('_').First().Replace(passwordLastKey.Split('_').First(), "Password") + "_" + passwordLastKey.Split('_').Last()).Replace("\\", "-");
        //    }
        //    else
        //    {
        //        passwordKey = (passwordKey + pathSeperator + passwordLastKey.Replace(passwordLastKey, "Password")).Replace("\\", "-");
        //    }

        //    string keyForPassword = string.Empty;
        //    XDocument pdfFile = XDocument.Load(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value);
        //    IEnumerable<XElement> pdfKeys = pdfFile.Root.Descendants().FirstOrDefault().Elements();

        //    if (pdfKeys.Any() && pdfKeys.Where(x => x.Attribute("key").Value.Equals(passwordKey, StringComparison.InvariantCultureIgnoreCase)).Any())
        //    {
        //        keyForPassword = pdfKeys.First(x => x.Attribute("key").Value.Equals(passwordKey, StringComparison.InvariantCultureIgnoreCase)).Attribute("key").Value;
        //    }

        //    XElement newNVPair = GetCorrespondingPasswordGV(passwordExistingKey);

        //    if (newNVPair != null)
        //    {
        //        if (updatedNVPairsElement.Elements().Where(x => x.Element(gvName).Value.Equals(newNVPair.Element(gvName).Value)).Any())
        //        {
        //            updatedNVPairsElement.Elements().Where(x => x.Element(gvName).Value.Equals(newNVPair.Element(gvName).Value)).Remove();
        //        }

        //        if (pdfKeys.Any() && pdfKeys.Where(x => x.Attribute("key").Value.Equals(passwordKey, StringComparison.InvariantCultureIgnoreCase)).Any())
        //        {
        //            keyForPassword = pdfKeys.First(x => x.Attribute("key").Value.Equals(passwordKey, StringComparison.InvariantCultureIgnoreCase)).Attribute("key").Value;
        //        }

        //        newNVPair.Element(gvValue).Value = pipe + keyForPassword + pipe;
        //        updatedNVPairsElement.Add(newNVPair);
        //    }

        //    if (passwordDic.Keys.Contains(passwordExistingKey, StringComparer.InvariantCultureIgnoreCase))
        //        passwordDic.Remove(passwordExistingKey);
        //}

        //private void GetPasswordForUser(string oldKey)
        //{
        //    string passwordKey = Path.GetDirectoryName(oldKey).Replace("\\", "/") + "/Password";

        //    if (passwordDic.Keys.Contains(passwordKey, StringComparer.InvariantCultureIgnoreCase))
        //    {
        //        string passKey = string.Empty;
        //        if (counter > 0)
        //            passKey = passwordKey + "_" + counter.ToString();
        //        else
        //            passKey = passwordKey;

        //        if (passwordDic.Keys.Contains(passwordKey, StringComparer.InvariantCultureIgnoreCase))
        //        {
        //            string passwordValue = passwordDic[passwordKey];

        //            if (!originalNVPairs.Keys.Contains(passKey, StringComparer.InvariantCultureIgnoreCase))
        //                originalNVPairs.Add(passKey, passwordValue);

        //            if (!propertiesKeyValuePairs.Keys.Contains(passKey, StringComparer.InvariantCultureIgnoreCase))
        //                propertiesKeyValuePairs.Add(passwordKey, (appendNVKeyText + passKey).Replace("/", "-"));

        //            if (!xmlKeyValuePairs.Keys.Contains(appendNVKeyText + passKey.Replace("/", "-"), StringComparer.InvariantCultureIgnoreCase))
        //                xmlKeyValuePairs.Add((appendNVKeyText + passKey).Replace("/", "-"), passwordValue);

        //            XElement newNVPair = GetCorrespondingPasswordGV(passwordKey);


        //            if (newNVPair != null && !originalNVPairs.Values.Contains(passwordValue, StringComparer.InvariantCultureIgnoreCase))
        //            {
        //                updatedNVPairsElement.Elements().Where(x => x.Element(gvName).Value.Equals(newNVPair.Element(gvName).Value)).Remove();
        //                newNVPair.Element(gvValue).Value = pipe + (appendNVKeyText + passKey).Replace("/", "-") + pipe;
        //                updatedNVPairsElement.Add(newNVPair);
        //            }
        //            else if (newNVPair != null && originalNVPairs.Values.Contains(passwordValue, StringComparer.InvariantCultureIgnoreCase))
        //            {
        //                updatedNVPairsElement.Elements().Where(x => x.Element(gvName).Value.Equals(newNVPair.Element(gvName).Value)).Remove();
        //                string keyForPassword = string.Empty;
        //                XDocument pdfFile = XDocument.Load(transformationPath + pathSeperator + environment.Key + pathSeperator + environment.Value);
        //                IEnumerable<XElement> pdfKeys = pdfFile.Root.Descendants().FirstOrDefault().Elements();

        //                if (pdfKeys.Any() && pdfKeys.Where(x => x.Value.Equals(passwordValue, StringComparison.InvariantCultureIgnoreCase)).Any())
        //                {
        //                    keyForPassword = pdfKeys.First(x => x.Value.Equals(passwordValue, StringComparison.InvariantCultureIgnoreCase)).Attribute("key").Value;
        //                }
        //                else if (originalNVPairs.Values.Contains(passwordValue, StringComparer.InvariantCultureIgnoreCase))
        //                {
        //                    keyForPassword = originalNVPairs.First(x => x.Value.Equals(passwordValue, StringComparison.InvariantCultureIgnoreCase)).Key;
        //                    keyForPassword = appendNVKeyText + keyForPassword.Replace("/", "-");
        //                }

        //                newNVPair.Element(gvValue).Value = pipe + keyForPassword + pipe;
        //                updatedNVPairsElement.Add(newNVPair);
        //            }
        //            if (passwordDic.Keys.Contains(passwordKey, StringComparer.InvariantCultureIgnoreCase))
        //                passwordDic.Remove(passwordKey);
        //        }
        //    }
        //}
        #endregion
    }
}