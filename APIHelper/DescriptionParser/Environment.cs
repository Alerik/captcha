using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DescriptionParser
{
	public class Environment
	{
		public object this[string key]
		{
			get
			{
				return environmentVariables[key];
			}
		}

		private Dictionary<string, object> environmentVariables = new Dictionary<string, object>();
		private Dictionary<string, GenerationTarget> environmentTargets = new Dictionary<string, GenerationTarget>();


		public List<FunctionDefinition> Functions => functionDefinitions;
		private List<FunctionDefinition> functionDefinitions = new List<FunctionDefinition>();
		private List<TableDefinition> tableDefinitions = new List<TableDefinition>();

		private List<SuperFunctionCall> SuperFunctions = new List<SuperFunctionCall>();
		private List<SuperTable> SuperTables = new List<SuperTable>();


		public Environment()
		{

		}

		public void RegisterEnvironmentVariable(string key, object obj)
		{
			environmentVariables[key] = obj;
		}

		public void RegisterEnvironmentTarget(string key, TargetDescription target)
		{
			this.environmentTargets[key.ToLower()] = new GenerationTarget(target);
		}

		public void RegisterFunctionDefinition(FunctionDefinition definition)
		{
			this.functionDefinitions.Add(definition);
		}

		public void RegisterTableDefinition(TableDefinition definition)
		{
			this.tableDefinitions.Add(definition);
		}

		public GenerationTarget GetGenerationTarget(string key)
		{
			if (this.environmentTargets.ContainsKey(key.ToLower()))
				return this.environmentTargets[key.ToLower()];
			throw new InvalidGenerationTargetException(key);
		}

		public string Require(string key)
		{
			if (environmentVariables.ContainsKey(key))
				return (environmentVariables[key] as string);
			else
				throw new EnvironmentVaraibleRequiredException(key);
		}
	}

	public class EnvironmentVaraibleRequiredException : Exception
	{
		public EnvironmentVaraibleRequiredException(string _key) : base($"The environment variable '{_key}' is required.") { }
	}
	public class InvalidGenerationTargetException : Exception
	{
		public InvalidGenerationTargetException(string _key) : base($"The generation target '{_key}' does not exist.") { }
	}
}