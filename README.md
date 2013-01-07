# GoodlyFere.Ektron.Linq Documentation

## Summary 

GoodlyFere.Ektron.Linq provides a very basic (at the moment) implementation of Linq for Ektron by translating Linq expressions
into valid `Ektron.Cms.Search.Expressions.Expression`s that then are submitted to the Ektron SearchManager via an
`AdvancedSearchCriteria` search.

## Basic Usage

To use this Linq-to-Ektron implementation you need three basic things in place:
- An Ektron 8.5 (this library has not been tested with any version <8.5) installation
- A class implementing the IEktronIdProvider interface (see the [AppSettingsIdProvider](#appsettingsidprovider-class) example below)
- One or more domain objects that map to data in your Ektron instance (see the [Widget](#widget-class) example class below)

### Example
    var widgets = EktronQueryFactory.Queryable<Widget>(new AppSettingsIdProvider());

    var query = from w in widgets
                where w.Id > 10
                select w;

    Widget[] itemWidgets = query.ToArray();

### Widget Class
This is a simple example of a domain object used to query Ektron content.  This Widget will
match any content item in Ektron. See more documentation to come soon on how to setup
domain objects to correspond to certain smart forms, folder paths and content types.

It has one property that matches an Ektron property: Id.  See more documentation to come soon on
setting up properties to match smart form, metadata and custom properties in Ektron.

    public class Widget
    {
        [EktronProperty(EkConstants.SearchProperties.ContentId,
            EktronExpressionType = typeof(IntegerPropertyExpression))]
        public long Id { get; set; }

        public string Name { get; set; }
    }

### AppSettingsIdProvider Class
You need to provide an implementation of `GoodlyFere.Ektron.Linq.Interfaces.IEktronIdProvider` when you call
`EktronQueryFactory.Queryable<T>()`.  The library will use your class to translate smart form and content
type names (that you provide as attributes on your domain object classes with the `SmartForm` and
`ContentType` attributes) into Ektron content IDs.  

With this simple implementation, the smart form or content type name is simply used (slightly modified) as a key for an appSetting
in your application's configuration file.  The value of the appSetting key is the Ektron content ID for the corresponding smart form
or content type.

    public class AppSettingsIdProvider : IEktronIdProvider
    {
        public long GetContentTypeId(string name)
        {
            EnsureValidName(name);
            string key = String.Concat(name, "ContentType");
            return GetId(key);
        }

        public long GetSmartFormId(string name)
        {
            EnsureValidName(name);
            string key = String.Concat(name, "SmartForm");
            return GetId(key);
        }

        private static long GetId(string key)
        {
            string value = ConfigurationManager.AppSettings[key];

            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentOutOfRangeException(
                    "key", String.Format("Could not find {0} in application settings.", key));
            }

            return Int64.Parse(value);
        }

        private void EnsureValidName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
        }
    }