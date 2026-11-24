using System.Collections.Generic;

namespace Mikita.Network.Messages.Registries;

/// <summary>
/// Assigns type IDs in registration order. Both endpoints must register the
/// same ordered list. Complete registrations before concurrent network use;
/// registration is not synchronized.
/// </summary>
public interface IMessageRegistry
	{
		/// <summary>
		/// Registers each label in enumeration order.
		/// </summary>
		void Register(IEnumerable<IMessageLabel> labels);

		/// <summary>
		/// Captures the description. Supply a nonblank, case-sensitive name
		/// and a defined role.
		/// Repeating a name and role returns the existing label. A different
		/// role or exhausted nonnegative short IDs throws
		/// InvalidOperationException.
		/// Role conflicts and exhausted IDs leave registrations unchanged.
		/// The input label is not retained or assigned an ID.
		/// </summary>
		IRegisteredMessageLabel Register(IMessageLabel label);

		/// <summary>
		/// Returns the existing label without registering anything.
		/// Throws KeyNotFoundException when the name is not registered.
		/// </summary>
		IRegisteredMessageLabel LabelOf(string typeName);

		/// <summary>
		/// Returns null for unknown IDs, including negative values.
		/// </summary>
		IRegisteredMessageLabel? LabelOf(MessageTypeId typeId);
	}