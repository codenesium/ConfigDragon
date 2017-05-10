# Config Dragon

Config Dragon is a command line utility you can use to set up configuration files in .NET solutions. 
It's similar to Slow Cheetah but I intend to add more complex feature that Slow Cheetah doesn't support.

#### Config Dragon will auto-detect your repository root for mercurial and GIT repositories if you have Source Tree installed.
#### For other installs you will have to set RepositoryRootDirectory

### Steps to use


#### Install the nuget package

There will be a ConfigDragon.bat and a ConfigDragon.json added to your project.

The default template looks like this.

`{
  "RepositoryRootDirectory": "",
  "ConfigActions": [
    {
      "Name": "Dev",
      "ConfigItems": [
        {
          "Name": "YOUR_PROJECT_NAME",
          "Directory": "PATH_RELATIVE_TO_RepositoryRootDirectory",
          "RelativeFilename": "app.config",
          "PackageName": "Development"
        }
      ]
    },
    {
      "Name": "Test",
      "ConfigItems": [
        {
          "Name": "YOUR_PROJECT_NAME",
          "Directory": "PATH_RELATIVE_TO_RepositoryRootDirectory",
          "RelativeFilename": "app.config",
          "PackageName": "Test"
        }
      ]
    },
    {
      "Name": "Prod",
      "ConfigItems": [
        {
          "Name": "YOUR_PROJECT_NAME",
          "Directory": "PATH_RELATIVE_TO_RepositoryRootDirectory",
          "RelativeFilename": "app.config",
          "PackageName": "Production"
        }
      ]
    }
  ],
  "ConfigPackages": [
    {
      "Name": "Development",
      "AppSettings": {
        "YOUR_APP_SETTING_KEY": "YOUR_APP_SETTING_VALUE"
      },
      "ConnectionStrings": {
        "YOUR_CONNECTION_STRING_NAME": "YOUR_CONNECTION_STRING_VALUE"
      }
    },
    {
      "Name": "Test",
      "AppSettings": {
        "YOUR_APP_SETTING_KEY": "YOUR_APP_SETTING_VALUE"
      },
      "ConnectionStrings": {
        "YOUR_CONNECTION_STRING_NAME": "YOUR_CONNECTION_STRING_VALUE"
      }
    },
    {
      "Name": "Production",
      "AppSettings": {
        "YOUR_APP_SETTING_KEY": "YOUR_APP_SETTING_VALUE"
      },
      "ConnectionStrings": {
        "YOUR_CONNECTION_STRING_NAME": "YOUR_CONNECTION_STRING_VALUE"
      }
    }
  ]
}`

Set up a configuration package that contains your connection string and app setting changes.

`{
      "Name": "Development",
      "AppSettings": {
        "YOUR_APP_SETTING_KEY": "YOUR_APP_SETTING_VALUE"
      },
      "ConnectionStrings": {
        "YOUR_CONNECTION_STRING_NAME": "YOUR_CONNECTION_STRING_VALUE"
      }
}`


#### Set up a config item.

`{
  "Name": "Dev",
  "ConfigItems": [
	{
	  "Name": "YOUR_PROJECT_NAME",
	  "Directory": "PATH_RELATIVE_TO_RepositoryRootDirectory",
	  "RelativeFilename": "app.config",
	  "PackageName": "Development"
	}
  ]
}`


To run the change call "ConfigDragon.bat dev" to apply the changes to your project. That's it!

You can have multiple config items call the same package. A scenario where that is needed is a solution with
multiple projects that need the same config change. 