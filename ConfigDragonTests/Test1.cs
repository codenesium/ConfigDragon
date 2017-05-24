namespace ConfigDragonTests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Codenesium.ConfigDragonLib;
    using FluentAssertions;
    using NUnit.Framework;

    /// <summary>
    /// Set of basic tests
    /// </summary>
    [TestFixture]
    public class Test1
    {
        /// <summary>
        /// Verifies that a loaded config sets all of the correct variables
        /// </summary>
        [Test]
        public void XmlManagerLoadConfig()
        {
            string testFile = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestFiles", "Config.json"));
            ConfigManager manager = new ConfigManager();
            var container = manager.LoadConfigFromString(testFile);
            container.Should().NotBeNull();

            container.GitExecutablePath.Should().Be("%USERPROFILE%\\AppData\\Local\\Atlassian\\SourceTree\\git_local\\bin\\git.exe");
            container.HgExecutablePath.Should().Be("%USERPROFILE%\\AppData\\Local\\Atlassian\\SourceTree\\hg_local\\hg.exe");
            container.RepositoryRootDirectory.Should().Be("test");

            container.ConfigActions.Should().HaveCount(1);

            container.ConfigPackages.Should().HaveCount(1);

            var action = container.ConfigActions.FirstOrDefault();

            action.Name.Should().Be("Dev");

            var configItem = action.ConfigItems.FirstOrDefault();

            configItem.Name.Should().Be("test");
            configItem.RelativeDirectory.Should().Be("ConfigDragon");
            configItem.TargetFilename.Should().Be("app.config");
            configItem.PackageName.Should().Be("Development");

            var configPackage = container.ConfigPackages.FirstOrDefault();

            configPackage.Name.Should().Be("Development");
            configPackage.AppSettings.Should().HaveCount(1);
            configPackage.ConnectionStrings.Should().HaveCount(1);
            configPackage.XmlSettings.Should().HaveCount(2);

            var appSetting = configPackage.AppSettings.FirstOrDefault();
            appSetting.Key.Should().Be("testAppSetting");
            appSetting.Value.Should().Be("YOUR_APP_SETTING_VALUE");

            var connectionString = configPackage.ConnectionStrings.FirstOrDefault();
            connectionString.Key.Should().Be("testConnectionString");
            connectionString.Value.Should().Be("YOUR_CONNECTION_STRING_VALUE");

            var xmlSetting1 = configPackage.XmlSettings.FirstOrDefault();
            xmlSetting1.Description.Should().Be("Sets the nlog level");
            xmlSetting1.Selector.Should().Be("/configuration/nlog:nlog/nlog:rules/nlog:logger[@writeTo='logfile']/@minlevel");
            xmlSetting1.Value.Should().Be("TRACE");
            xmlSetting1.Namespaces.Should().HaveCount(1);

            var namespaceItem = xmlSetting1.Namespaces.FirstOrDefault();
            namespaceItem.Key.Should().Be("nlog");
            namespaceItem.Value.Should().Be("http://www.nlog-project.org/schemas/NLog.xsd");
        }

        /// <summary>
        /// Tests that the xml transform works
        /// </summary>
        [Test]
        public void XmlManagerProcessConfigXmlSetting()
        {
            string testConfigFile = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestFiles", "Config.json"));
            string testAppConfigFile = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestFiles", "app.config"));

            XmlFileManager manager = new XmlFileManager();
            var result = manager.Process(testAppConfigFile, new XmlSetting("test", "/configuration/nlog:nlog/nlog:rules/nlog:logger[@writeTo='logfile']/@minlevel", "DEBUG", new Dictionary<string, string>() { { "nlog", "http://www.nlog-project.org/schemas/NLog.xsd" } }));
            result.Success.Should().BeTrue();
            result.Content.Should().NotBeNullOrWhiteSpace();
            result.Content.Should().Contain(@"<logger name=""*"" minlevel=""DEBUG"" writeTo=""logfile"" />");
        }

        /// <summary>
        /// Tests that an invalid XPath selector fails
        /// </summary>
        [Test]
        public void XmlManagerProcessConfigXmlSetting_InvalidSelector()
        {
            string testConfigFile = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestFiles", "Config.json"));
            string testAppConfigFile = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestFiles", "app.config"));

            XmlFileManager manager = new XmlFileManager();
            var result = manager.Process(testAppConfigFile, new XmlSetting("test", "/configuration/nlog:nlog/nlog:rules/nlog:logger[@writeTo='logfile']/invalid", "DEBUG", new Dictionary<string, string>() { { "nlog", "http://www.nlog-project.org/schemas/NLog.xsd" } }));
            result.Success.Should().BeFalse();
        }
    }
}