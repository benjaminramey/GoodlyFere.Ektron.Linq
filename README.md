# LINQ to Ektron Search

## Table of Contents
- [Summary](#summary)
- [Installation](#installation)
- [Basic Usage](#basic-usage)
- [Reference](#reference)
    - [Class Attributes](#class-attributes)
    - [Property Attributes](#property-attributes-goodlyfereektronlinqmodelattributes)
    - [Return Properties](#return-properties)
- [Known Issues](#known-issues)
- [Versioning](#versioning)

## Summary 

LINQ to Ektron Search provides a very basic (at the moment) implementation of LINQ for Ektron by translating LINQ expressions
into valid `Ektron.Cms.Search.Expressions.Expression`s that then are submitted to the Ektron SearchManager via an
`AdvancedSearchCriteria` search.

If you use LINQ to Ektron Search in a project, please let me know!  I'd be excited to hear about it.

I learned a ton from [Chris Eldridge](https://github.com/chriseldredge)'s [LINQ to Lucene](https://github.com/themotleyfool/Lucene.Net.Linq) project that helped me take a good path on this project.
So, thanks Chris!

## Installation

I recommend installing via the NuGet package through Visual Studio.  See generic instructions here:
[NuGet package installation instructions](http://docs.nuget.org/docs/start-here/managing-nuget-packages-using-the-dialog). The
best way to find the package through the NuGet interface is to search for "ektron" or "goodlyfere".  If you search for anything
with "linq" a bunch of other stuff will come up.

You can also download the source code and build your DLLs from that.  However, to do so, you will need to reference several
Ektron DLLs not included in this repository.  You can see which ones you'll need by looking at the broken references
in the GoodlyFere.Ektron.Linq project.

## Basic Usage

To use this library you need two basic things in place:
- An Ektron 8.5 (this library has not been tested with any version <8.5) installation with search running.  See Ektron documentation on 
setting this up.
- One or more domain objects that map to indexed properties in your Ektron instance (see the [Widget](#widget-class) example class below)

### Example
    var widgets = EktronQueryFactory.Queryable<Widget>(new AppSettingsIdProvider());

    var query = from w in widgets
                where w.Id > 10
                select w;

    Widget[] itemWidgets = query.ToArray();

### Widget Class
This is a simple example of a domain object used to query Ektron content.  This Widget will
match any content item in Ektron. It has one property that matches an Ektron property: Id.

See the class and property attribute
documentation below for more details on setting up classes to match smart forms, folder paths,
content types and setting up properties to match smart form properties, meta data properties, 
regular properties and custom properties.

    public class Widget
    {
        [EktronIntegerProperty(EkConstants.SearchProperties.ContentId)]
        public long Id { get; set; }

        public string Name { get; set; }
    }

### AppSettingsIdProvider Class
You can provide an implementation of `GoodlyFere.Ektron.Linq.Interfaces.IEktronIdProvider` when you call
`EktronQueryFactory.Queryable<T>()`.  If you do not, the class detailed below will be used by default.
The library will use your class to translate smart form and content
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

#### Defaults
You may use any property (without any type of GoodlyFere.Ektron.Linq.Model.Attributes.* attribute
attached) in a LINQ expression to search in Ektron.  If you do not supply an attribute, the library will
use the name of the property and assume it is a StringPropertyExpression (see above in the
[Return Properties](#Return-Properties) section for information
on how these properties are handled in the search results).  This may be updated in the future
to assume a PropertyExpression type from the actual property type (so that a `long` property, for example, 
would auto-translate into an IntegerPropertyExpression instead of a StringPropertyExpression).

Note that if your property name does not exactly (except for casing) match a property in Ektron,
Ektron's search manager will throw an exception and you will receive an empty results list.

For example, suppose you define an Id property on a class like this:

    public class ObjectWithId
    {
        public long Id { get; set; }
    }

If you use Id in a query, Ektron will throw an exception because the real name of the Id property in Ektron
is "contentid".  Renaming your property to "ContentId" as following would work:

    public class ObjectWithId
    {
        public long ContentId { get; set; }
    }

#### Using Attributes
The have more fine-tuned control over how the library builds your queries from your class properties, use
the attributes detailed below.

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
This is the base class for all of the property attributes.  From it, you can construct the equivalent of all other
property attributes.

##### EktronPropertyName
The name of the Ektron property that this class property maps to.  For a regular, custom or metadata property, this must be the exact
name of the corresponding Ektron property.  For a smart form property, this must be the absolute path to the
element in the SmartForm.

##### EktronExpressionType
This is the type of Ektron.Cms.Search.Expressions.PropertyExpression that this property should be translated
into by the library when constructing the AdvancedSearchCriteria.ExpressionTree. 

All types of PropertyExpressions in Ektron 8.5 have convenience attributes as described above.  For example,
setting `EktronExpressionType = typeof(StringPropertyExpression)` it equivalent to using an
EktronStringPropertyAttribute.

##### IsSmartFormProperty
When set to true, the EktronPropertyName will be converted using the corresponding `SearchSmartFormProperty.Get*Property()`
method when the search expression tree is built.  Therefore, the EktronPropertyName should be the full path
to the SmartForm attribute you are mapping to.

##### IsMetadataProperty
When set to true, the EktronPropertyName will be converted using the corresponding `SearchMetadataProperty.Get*Property()`
method when the search expression tree is built.

##### IsCustomProperty
When set to true, the EktronPropertyName will be converted using the corresponding `SearchCustomProperty.Get*Property()`
method when the search expression tree is built.

#### SmartFormPropertyAttribute
The SmartFormPropertyAttribute is a convenience attribute equivalent to setting `IsSmartFormProperty = true` on
an EktronPropertyAttribute.

#### CustomPropertyAttribute
The CustomPropertyAttribute is a convenience attribute equivalent to setting `IsCustomProperty = true` on
an EktronPropertyAttribute.

#### MetadataPropertyAttribute
The MetadataPropertyAttribute is a convenience attribute equivalent to setting `IsMetadataProperty = true` on
an EktronPropertyAttribute.

### Return Properties
If you're familiar with using Ektron's AdvancedSearchCriteria, you know you have to define which
properties you want returned by adding them to the AdvancedSearchCriteria.ReturnProperties property.

As you may suspect, this is what the library populates your domain object properties from.  The library does
this essentially in a two-step process:

1. It collects the properties you use in the where and order-by clauses and adds these to the ReturnProperties.
2. It collects all of the properties on your domain object that have any kind of EktronPropertyAttribute
attached to them.

The reasoning behind this is pretty simple: the Ektron SearchManager will throw an exception if any properties
you use in the AdvancedSearchCriteria.ExpressionTree or AdvancedSearchCriteria.ReturnProperties are not
indexed and will return no results.  So, the library makes the safest assumption it can, while being as
broading inclusive as it can: if you are searching on a property (where clause) or ordering by a property
(order-by clause) or attach an EktronPropertyAttribute to a property, then you have also made sure that
this property is indexed in Ektron.

This has two simple consequences:

1. You can have any number of properties in your domain object, without
EktronPropertyAttributes, that will be ignored by the library (unless you include them in the where or 
order-by clauses) when your LINQ query is parsed.
2. Any property without an EktronPropertyAttribute and not used in the where or order-by clauses will
not be populated with data from Ektron.

#### Example
Consider a query using the [Widget class](#widget-class) above.  The following query will NOT populate
the `Name` property on any of the `Widgets` in `itemWidgets`:

    var query = from w in widgets
                where w.Id > 10
                select w;

    Widget[] itemWidgets = query.ToArray();

This query WILL attempt to populate the `Name` property:

    var query = from w in widgets
                where w.Name == "bob"
                select w;

    Widget[] itemWidgets = query.ToArray();

However, it will cause the SearchManager to throw an exception and return no results because the `Name` 
property does not exist as a standard Ektron content property.

## Known Issues
- **Null comparisons**  
There seems to be an issue in Ektron 8.5 with PropertyExpression.IsNull() and PropertyExpression.IsNotNull() so
that, no matter how you construct it, they will cause the SearchManager to throw an expression when you try to search
with any number of these expressions in the ExpressionTree.  Therefore, the recent Nullable property support I 
just added (1.0.11.103) is pretty useless.  If you use NullableProperty.HasValue or AnyProperty == null in a LINQ where clause
the library will convert this to a IsNotNullExpression or IsNullExpression and your search will fail.  So, beware!

## Versioning
This is the first piece of software I've written where versioning could potentially actually matter.  So, I had
to do some reading on how to actually go about it.  There are lots of ideas out there, and really, it's kind of confusing.

So far, I've started with something like this for the four version dots:
- Major version
- Minor version
- Feature build (updated every time I code a significant feature)
- Bug fix commit (updated almost every time I commit, definitely every time I commit a bug fix)

After 1.0.10.7, I am changing this to:
- Major version (will correspond to Ektron's major version.  So, 1 = 8 right now)
- Minor version (will correspond to Ektron's minor version.  So, 0 = 5 right now)
- Feature set number (will generally correspond to NuGet package builds)
- Git commit number

All of the above applies to the AssemblyFileVersion version number.  I am keeping the AssemblyVersion at
1.0.0.0 for now where the major and minor version numbers will work just like the major and minor version numbers in the
AssemblyFileVersion.  So, in the future, if I or someone else updates the library to work with new Ektron 8.6 features (honestly,
I'm not even sure if there are any that would affect this library), I will create an 8.5 (1.0.0.0) branch and the master
branch will become the 8.6 branch with a new, updated AssemblyVersion of 1.1.0.0.  

### Version History
- (1.0.11.103) Added nullable properties to return properties when used in where clause
- (1.0.11.102) Fixed bug with compound where clauses made up of nullable property comparisons
- (1.0.11.100) Added support for nullable properties, including the HasValue property
- (1.0.10.98) Updated versioning scheme to correspond to git commits, no code updates
- (1.0.10.7) Updated to re-linq 1.13.182.0
- (1.0.10.6) Added lots of class, property and method documentation
- (1.0.10.5) Added support for string.IsNullOrEmpty method
- (1.0.9.5) Added inclusion of non-attributed properties into ReturnProperties when used in the Where clause of a query
- (1.0.9.4) Added FormattingExpressionVisitor as an easy way to format Ektron expression trees into a string.
- (1.0.9.3) Removed debugging log messages from ReflectionExtensions. 
- (1.0.9.2) Constricted the EktronExpressionType property on EktronPropertyAttribute to throw an exception if it is not a
type assignable to Ektron.Cms.Search.Expressions.PropertyExpression.
- (1.0.9.1) Updated ReflectionExtensions to use the built-in MemoryCache instead of just a static dictionary.
- (1.0.9.0) Added support for Any, Count and LongCount result operator LINQ methods.
- (1.0.8.2) Added an overload method for EktronQueryFactory.Queryable<T> to use a default IdProvider (AppSettingsIdProvider).
- (1.0.8.1) Fixed bug when a constant in a comparison was null.
- (1.0.8.0) Added convenience Ektron property attributes to avoid specifying the EktronExpressionType explicitly
- (1.0.7.1) Fixed bugs in PropertyMapBase having to do with passing a null value to FirstOrDefault.
- (1.0.7.0) Added support for unary type-casting.  For example, `(long)w.Id == 1L`.
- (1.0.6.0) Added support for unary not expressions.  For example, `!(w.Id == 1)`.
