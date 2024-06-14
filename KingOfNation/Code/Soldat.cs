using System;

public abstract class Soldat {
	/// <summary>
	/// Le nom du soldat
	/// </summary>
	public abstract string Nom { get; }
	public abstract string Description { get; }
	public abstract int Nb { get; set; }

}
