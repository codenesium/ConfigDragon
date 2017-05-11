# Config Dragon

Config Dragon is a command line utility you can use to set up configuration files in .NET solutions. 
It's similar to Slow Cheetah but I intend to add more complex feature that Slow Cheetah doesn't support.

#### Config Dragon will auto-detect your repository root for mercurial and GIT repositories if you have Source Tree installed.

#### For other installs you will have to set RepositoryRootDirectory

#### Currently modification of application settings, connection strings, xml in visual studio project files are supported


### Steps to use


#### Install the nuget package

Install-Package Codenesium.ConfigDragon

There will be a ConfigDragon.bat and a ConfigDragon.json added to your project.

The default template looks like this.

```javascript
{
  "RepositoryRootDirectory": "",
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
      "VisualStudioProjectSettings": {
        "/ns:Project/ns:ProjectExtensions/ns:VisualStudio/ns:FlavorProperties/ns:WebProjectProperties/ns:IISUrl": "http://localhost:50000"
      }
    },
    {
      "Name": "ProductionPackage",
      "AppSettings": {
        "YOUR_APP_SETTING_KEY": "YOUR_APP_SETTING_VALUE"
      },
      "ConnectionStrings": {
        "YOUR_CONNECTION_STRING_NAME": "YOUR_CONNECTION_STRING_VALUE"
      },
      "VisualStudioProjectSettings": {
        "/ns:Project/ns:ProjectExtensions/ns:VisualStudio/ns:FlavorProperties/ns:WebProjectProperties/ns:IISUrl": "http://localhost:50000"
      }
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
      "VisualStudioProjectSettings": {
        "/ns:Project/ns:ProjectExtensions/ns:VisualStudio/ns:FlavorProperties/ns:WebProjectProperties/ns:IISUrl": "http://localhost:50000"
      }
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

The Visual Studio project modification uses XPAth 1.0 to select the item you want to edit. Note that ns: is required to 
namespace the selectors correctly. 