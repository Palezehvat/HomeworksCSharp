using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class Test : Attribute
{
    public Type? Expected { get; set; }

    public string? Ignored { get; set; }
}

[AttributeUsage(AttributeTargets.All)]
public class BeforeClass : Attribute
{

}

[AttributeUsage(AttributeTargets.All)]
public class AfterClass : Attribute
{

}

[AttributeUsage(AttributeTargets.All)]
public class Before : Attribute
{
    [Before]
    public void Method()
    {

    }
}

[AttributeUsage(AttributeTargets.All)]
public class After : Attribute
{
    [After]
    public void Method()
    {

    }
}