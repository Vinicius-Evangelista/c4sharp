﻿using C4Sharp.Extensions;

namespace C4Sharp.Models;

/// <summary>
/// Not Docker! In the C4 model, a container represents an application or a data store. A container is something
/// that needs to be running in order for the overall software system to work. In real terms, a container is
/// something like:
///
/// Server-side web application, Client-side web application, Client-side desktop application,
/// Mobile app, Server-side console application, Serverless function, Database, Blob or content store,
/// File system, Shell script
///
/// <see href="https://c4model.com/#ContainerDiagram"/>
/// </summary>
public record Container : Structure
{
    private readonly Dictionary<string, Container> _instances = new();

    public ContainerType ContainerType { get; init; }
    public string? Technology { get; init; }
    public Container this[int index] => GetInstance(index.ToString());
    public Container this[string instanceName] => GetInstance(instanceName);
    
    public Container(string alias, string label) : base(alias, label)
    {
    }

    protected Container(StructureIdentity identity, string label) : base(identity, label)
    {
    }

    /// <summary>
    /// Get or Create a instance of current container
    /// </summary>
    /// <param name="instanceName">instance name</param>
    /// <returns>New Container</returns>
    private Container GetInstance(string instanceName)
    {
        var id = new StructureIdentity(Alias, instanceName);

        _instances.TryGetValue(id.Value, out var instance);
        
        var container = instance ?? new Container(id, Label)
        {
            ContainerType = ContainerType,
            Description = Description,
            Technology = Technology,
            Boundary = Boundary,
            Tags = Tags
        };

        if (instance is null)
            _instances[id.Value] = container;
        
        return container;
    }
}

public record Container<T> : Container
{
    private protected Container(ContainerType containerType, string technology, string description)
        : base(StructureIdentity.New<T>(), typeof(T).ToNamingConvention())
    {
        ContainerType = containerType;
        Technology = technology;
        Description = description;
    }
}