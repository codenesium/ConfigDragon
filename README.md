# Config Dragon

Config Dragon is a command line utility you can use to set up configuration files in .NET solutions. 

#### Config Dragon will auto-detect your repository root for mercurial and GIT repositories if you have Source Tree installed. If you have another source control system you can set the location of the hg.exe or git.exe in the ConfigDragon.json

#### For other installs you will have to set RepositoryRootDirectory

#### Modification of application settings, connection strings, generic strings and XML files are supported

### Steps to use

#### Install the nuget package

Install-Package Codenesium.ConfigDragon

There will be a ConfigDragon.bat and a ConfigDragon.json added to your project.

The default template looks like this.

```javascript
{
  "RepositoryRootDirectory": "",
  "HgExecutablePath": "%USERPROFILE%\\AppData\\Local\\Atlassian\\SourceTree\\hg_local\\hg.exe",
  "GitExecutablePath": "%USERPROFILE%\\AppData\\Local\\Atlassian\\SourceTree\\git_local\\bin\\git.exe",
  "ConfigActions": [
    {
      "Name": "Dev",
      "ConfigItems": [
        {
          "Name": "AppConfigChange",
          "RelativeDirectory": "PATH_RELATIVE_TO_RepositoryRootDirectory",
          "TargetFilename": "app.config",
          "PackageName": "DevelopmentPackage"
        }
      ]
    },
    {
      "Name": "Prod",
      "ConfigItems": [
        {
          "Name": "AppConfigChange",
          "RelativeDirectory": "PATH_RELATIVE_TO_RepositoryRootDirectory",
          "TargetFilename": "app.config",
          "PackageName": "ProductionPackage"
        }
      ]
    }
  ],
  "ConfigPackages": [
    {
      "Name": "DevelopmentPackage",
      "AppSettings": {
        "YOUR_APP_SETTING_KEY": "YOUR_APP_SETTING_VALUE"
      },
      "ConnectionStrings": {
        "YOUR_CONNECTION_STRING_NAME": "YOUR_CONNECTION_STRING_VALUE"
      },
     "XmlSettings": [
      {
        "Selector": "/configuration/nlog:nlog/nlog:rules/nlog:logger[@writeTo='logfile']/@minlevel",
        "Value": "TRACE",
        "Description": "Sets the nlog level",
        "Namespaces": {
          "nlog": "http://www.nlog-project.org/schemas/NLog.xsd"
        }
      },
        {
        "Selector": "/vs:Project/vs:ProjectExtensions/vs:VisualStudio/vs:FlavorProperties/vs:WebProjectProperties/vs:IISUrl",
        "Value": "http://localhost:8000",
        "Description": "Sets the local IIS express url to localhost",
        "Namespaces": {
          "vs": "http://schemas.microsoft.com/developer/msbuild/2003"
        }
      }
      ]
    },
    {
      "Name": "ProductionPackage",
      "AppSettings": {
        "YOUR_APP_SETTING_KEY": "YOUR_APP_SETTING_VALUE"
      },
      "ConnectionStrings": {
        "YOUR_CONNECTION_STRING_NAME": "YOUR_CONNECTION_STRING_VALUE"
      },
      "XmlSettings": [
        {
            "Selector": "/configuration/nlog:nlog/nlog:rules/nlog:logger[@writeTo='logfile']/@minlevel",
            "Value": "TRACE",
            "Description": "Sets the nlog level",
            "Namespaces": {
              "nlog": "http://www.nlog-project.org/schemas/NLog.xsd"
            }
          },
          {
            "Selector": "/vs:Project/vs:ProjectExtensions/vs:VisualStudio/vs:FlavorProperties/vs:WebProjectProperties/vs:IISUrl",
            "Value": "http://localhost:8000",
            "Description": "Sets the local IIS express url to localhost",
            "Namespaces": {
              "vs": "http://schemas.microsoft.com/developer/msbuild/2003"
          }
        }
      ],
      "StringSettings": [
        {
          "Needle": "value=\"123456\"",
          "ReplacementValue": "value=\"test\"",
          "Description": "Replace a setting"
        }
      ]
    }
  ]
}
```

Set up a configuration package that contains your connection string and app setting changes.

```javascript
   {
      "Name": "DevelopmentPackage",
      "AppSettings": {
        "YOUR_APP_SETTING_KEY": "YOUR_APP_SETTING_VALUE"
      },
      "ConnectionStrings": {
        "YOUR_CONNECTION_STRING_NAME": "YOUR_CONNECTION_STRING_VALUE"
      },
     "XmlSettings": [
      {
        "Selector": "/configuration/nlog:nlog/nlog:rules/nlog:logger[@writeTo='logfile']/@minlevel",
        "Value": "TRACE",
        "Description": "Sets the nlog level",
        "Namespaces": {
          "nlog": "http://www.nlog-project.org/schemas/NLog.xsd"
        }
      },
        {
        "Selector": "/vs:Project/vs:ProjectExtensions/vs:VisualStudio/vs:FlavorProperties/vs:WebProjectProperties/vs:IISUrl",
        "Value": "http://localhost:8000",
        "Description": "Sets the local IIS express url to localhost",
        "Namespaces": {
          "vs": "http://schemas.microsoft.com/developer/msbuild/2003"
        }
      }
      ],
      "StringSettings": [
        {
          "Needle": "value=\"123456\"",
          "ReplacementValue": "value=\"test\"",
          "Description": "Replace a setting"
        }
      ]
    }
```


#### Set up a config item.

```javascript
{
  "Name": "Dev",
  "ConfigItems": [
	{
	  "Name": "YOUR_PROJECT_NAME",
	  "RelativeDirectory": "PATH_RELATIVE_TO_RepositoryRootDirectory",
	  "TargetFilename": "app.config",
	  "PackageName": "DevelopmentPackage"
	}
  ]
}
```


To run the change call "ConfigDragon.bat dev" to apply the changes to your project. That's it!

You can have multiple config items call the same package. A scenario where that is needed is a solution with
multiple projects that need the same config change. 

XML changes use an XPath 1.0 selector to find the node to modify. An XML setting has a key/value set of
namespaces that you can add if your XML uses namespaces. https://www.w3schools.com/xml/xpath_examples.asp has 
a simple tutorial on how XPath works. 


String replacement works like you would expect. It looks for a string and replaces it with another string. This is 
useful for setting variables in javascript files. The string must be escaped and be valid JSON to work work. 


```javascript
"StringSettings": [
	{
	  "Needle": "value=\"123456\"",
	  "ReplacementValue": "value=\"test\"",
	  "Description": "Replace a setting"
	}
]
```

In this example we would search for value="123456" and replace it with value="test".
