using System;
using System.Collections.Generic;
using System.Reflection;



/*
 * This class exists to solve a very specific Unity problem:

“At runtime (or editor-time), I want to find every type in my project that implements some interface, without manually registering them.”

Example uses:

find all IEvent types

find all ICommand / IQuestCondition / IGoapAction implementations

auto-register systems, factories, serializers, validators, etc.

Unity doesn’t give you a neat built-in “give me all classes implementing interface X in my scripts” API, so people use reflection over assemblies.
 * 
 */

/// <summary>
/// A utility class, PredefinedAssemblyUtil, provides methods to interact with predefined assemblies.
/// It allows to get all types in the current AppDomain that implement from a specific Interface type.
/// For more details, <see href="https://docs.unity3d.com/2023.3/Documentation/Manual/ScriptCompileOrderFolders.html">visit Unity Documentation</see>
/// </summary>
public static class PredefinedAssemblyUtil
{
    /// <summary>
    /// Enum that defines the specific predefined types of assemblies for navigation.
    /// </summary>    
    enum AssemblyType
    {
        AssemblyCSharp,
        AssemblyCSharpEditor,
        AssemblyCSharpEditorFirstPass,
        AssemblyCSharpFirstPass
    }

    /// <summary>
    /// Maps the assembly name to the corresponding AssemblyType.
    /// </summary>
    /// <param name="assemblyName">Name of the assembly.</param>
    /// <returns>AssemblyType corresponding to the assembly name, null if no match.</returns>
    static AssemblyType? GetAssemblyType(string assemblyName)
    {
        return assemblyName switch
        {
            "Assembly-CSharp" => AssemblyType.AssemblyCSharp,
            "Assembly-CSharp-Editor" => AssemblyType.AssemblyCSharpEditor,
            "Assembly-CSharp-Editor-firstpass" => AssemblyType.AssemblyCSharpEditorFirstPass,
            "Assembly-CSharp-firstpass" => AssemblyType.AssemblyCSharpFirstPass,
            _ => null
        };
    }

    /// <summary>
    /// Method looks through a given assembly and adds types that fulfill a certain interface to the provided collection.
    /// </summary>
    /// <param name="assemblyTypes">Array of Type objects representing all the types in the assembly.</param>
    /// <param name="interfaceType">Type representing the interface to be checked against.</param>
    /// <param name="results">Collection of types where result should be added.</param>
    static void AddTypesFromAssembly(Type[] assemblyTypes, Type interfaceType, ICollection<Type> results)
    {
        if (assemblyTypes == null) return;
        for (int i = 0; i < assemblyTypes.Length; i++)
        {
            Type type = assemblyTypes[i];
            /*IsAssignableFrom is the key: it returns true if type implements interfaceType (or derives from it if it’s a base class).

            So GetTypes(typeof(IMyInterface)) returns a list of all concrete classes that implement IMyInterface in the selected assemblies.
             * 
             */
            if (type != interfaceType && interfaceType.IsAssignableFrom(type))
            {
                results.Add(type);
            }
        }
    }

    /// <summary>
    /// Gets all Types from all assemblies in the current AppDomain that implement the provided interface type.
    /// </summary>
    /// <param name="interfaceType">Interface type to get all the Types for.</param>
    /// <returns>List of Types implementing the provided interface type.</returns>    
    /// 
    /*
    public static List<Type> GetTypes(Type interfaceType)
    {
        //That gets everything loaded right now (UnityEngine, your scripts, packages, etc.).
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        Dictionary<AssemblyType, Type[]> assemblyTypes = new Dictionary<AssemblyType, Type[]>();
        List<Type> types = new List<Type>();
        for (int i = 0; i < assemblies.Length; i++)
        {
            AssemblyType? assemblyType = GetAssemblyType(assemblies[i].GetName().Name);
            if (assemblyType != null)
            {
                assemblyTypes.Add((AssemblyType)assemblyType, assemblies[i].GetTypes());
            }
        }

        assemblyTypes.TryGetValue(AssemblyType.AssemblyCSharp, out var assemblyCSharpTypes);
        AddTypesFromAssembly(assemblyCSharpTypes, interfaceType, types);

        assemblyTypes.TryGetValue(AssemblyType.AssemblyCSharpFirstPass, out var assemblyCSharpFirstPassTypes);
        AddTypesFromAssembly(assemblyCSharpFirstPassTypes, interfaceType, types);

        return types;
    }*/

    public static List<Type> GetTypes(Type interfaceType)
    {
        var results = new List<Type>();
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in assemblies)
        {
            // Skip Unity / System assemblies
            var name = assembly.GetName().Name;
            if (name.StartsWith("Unity")) continue;
            if (name.StartsWith("System")) continue;
            if (name.StartsWith("Microsoft")) continue;
            if (name.StartsWith("netstandard")) continue;

            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                types = e.Types;
            }

            foreach (var type in types)
            {
                if (type == null) continue;
                if (type.IsInterface || type.IsAbstract) continue;

                if (interfaceType.IsAssignableFrom(type))
                    results.Add(type);
            }
        }

        return results;
    }
}



//The real “why make this class?” (what problem it enables)

/*Auto-registration / discovery

Instead of this:

eventBus.Register<PlayerDiedEvent>();
eventBus.Register<ItemPickedUpEvent>();
eventBus.Register<QuestCompletedEvent>();
// forever...

You do:

var eventTypes = PredefinedAssemblyUtil.GetTypes(typeof(IEvent));
foreach (var t in eventTypes) RegisterEventType(t);


Same for factories:

find all IQuestCondition implementations

build a dictionary {conditionType => handler}

done.

This is a “plugin architecture” pattern: add a new class and it’s discovered automatically.

 */