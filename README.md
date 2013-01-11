# LINQ to Ektron Search

## Summary 

LINQ to Ektron Search provides a very basic (at the moment) implementation of LINQ for Ektron by translating LINQ expressions
into valid `Ektron.Cms.Search.Expressions.Expression`s that then are submitted to the Ektron SearchManager via an
`AdvancedSearchCriteria` search.

## Installation

I recommend installing via the NuGet package through Visual Studio.  See generic instructions here:
[NuGet package installation instructions](http://docs.nuget.org/docs/start-here/managing-nuget-packages-using-the-dialog). The
best way to find the package through the NuGet interface is to search for "ektron" or "goodlyfere".  If you search for anything
with "linq" a bunch of other stuff will come up.

You can also download the source code and build your DLLs from that.  However, to do so, you will need to reference several
Ektron DLLs not included in this repository.  You can see which ones you'll need by looking at the broken references
in the GoodlyFere.Ektron.Linq project.

## Basic Usage

To use this Linq-to-Ektron implementation you need three basic things in place:
- An Ektron 8.5 (this library has not been tested with any version <8.5) installation with search running.  See Ektron documentation (good luck!) on 
setting this up.
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
match any content item in Ektron.

It has one property that matches an Ektron property: Id.  See more documentation to come soon on
setting up properties to match smart form, metadata and custom properties in Ektron.

    public class Widget
    {
        [EktronIntegerProperty(EkConstants.SearchProperties.ContentId)]
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

## Reference

### Class Attributes (GoodlyFere.Ektron.Linq.Model.Attributes)
#### SmartFormAttribute

Use this attribute to designate a domain object class to correspond to a smart form in Ektron.  This will tell 
the library to add a `SearchContentProperty.XmlConfigId == <smart form id>` clause to the AdvancedSearchCriteria
expression tree.

    [SmartForm("Doctor")]
    internal class SmartFormWidget
    {
        [EktronIntegerProperty(EkConstants.SearchProperties.ContentId)]
        public long Id { get; set; }
    }

The name that you pass to the SmartFormAttribute is the name that your IEktronIdProvider will use to find the ID
for the smart form. Therefore, this name can be an arbitrary value as long as you know how to use it in your 
IEktronIdProvider implementation.

#### ContentTypeAttribute

Use this attribute to designate a domain object class to correspond to a content type in Ektron.  This will tell 
the library to add a `SearchContentProperty.ContentType == <content type id>` clause and a 
`SearchContentProperty.ContentSubType == <content sub type id>` (if designated) to the AdvancedSearchCriteria
expression tree.

    [ContentType("ContentTypeName")]
    internal class ContentTypeWidget
    {
        [EktronIntegerProperty(EkConstants.SearchProperties.ContentId)]
        public long Id { get; set; }
    }

    [ContentType("ContentTypeName", ContentSubTypeName = "SubTypeName")]
    internal class SubContentTypeWidget
    {
        [EktronIntegerProperty(EkConstants.SearchProperties.ContentId)]
        public long Id { get; set; }
    }

The name that you pass to the ContentTypeAttribute is the name that your IEktronIdProvider will use to find the ID
for the content type. Therefore, this name can be an arbitrary value as long as you know how to use it in your 
IEktronIdProvider implementation.

#### FolderPathAttribute

Use this attribute to designate a domain object class to correspond to a certain folder path in Ektron.  This will tell 
the library to add a `SearchContentProperty.FolderPath == <folder path>` clause to the AdvancedSearchCriteria
expression tree.

    [FolderPath("Folders/Widgets")]
    internal class FolderPathWidget
    {
        [EktronIntegerProperty(EkConstants.SearchProperties.ContentId)]
        public long Id { get; set; }
    }

### Property Attributes (GoodlyFere.Ektron.Linq.Model.Attributes)

Each of the attributes detailed below has the following convenience attributes which let you avoid specifying
the EktronExpressionType property explicitly.
- \*type\*BooleanPropertyAttribute
- \*type\*DatePropertyAttribute
- \*type\*DecimalPropertyAttribute
- \*type\*IntegerPropertyAttribute
- \*type\*StringPropertyAttribute

For example, for smart form properties there are the following convenience attributes:
- SmartFormBooleanPropertyAttribute
- SmartFormDatePropertyAttribute
- SmartFormDecimalPropertyAttribute
- SmartFormIntegerPropertyAttribute
- SmartFormStringPropertyAttribute

#### EktronPropertyAttribute
#### SmartFormPropertyAttribute
#### CustomPropertyAttribute
#### MetadataPropertyAttribute

## Version History
- (1.0.8.1) Fixed bug when a constant in a comparison was null.
- (1.0.8.0) Added convenience Ektron property attributes to avoid specifying the EktronExpressionType explicitly
- (1.0.7.1) Fixed bugs in PropertyMapBase having to do with passing a null value to FirstOrDefault.
- (1.0.7.0) Added support for unary type-casting.  For example, `(long)w.Id == 1L`.
- (1.0.6.0) Added support for unary not expressions.  For example, `!(w.Id == 1)`.