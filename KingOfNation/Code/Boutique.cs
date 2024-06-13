using System;

public class Boutique : Batiment  {
	/// <summary>
	/// La ressource que va g�n�rer le bat�ment
	/// </summary>
	private Production? ressource;
	public Production Ressource {
		get {
			return ressource;
		}
	}

	/// <summary>
	/// Cr�er le bat�ment
	/// </summary>
	public void Creer() {
		throw new System.NotImplementedException("Not implemented");
	}
	/// <summary>
	/// G�n�re la ressource mis en param�tre
	/// </summary>
	/// <param name="Ressource">La ressource que va g�n�rer le bat�ment</param>
	public void Generer(Production ressource) {
		throw new System.NotImplementedException("Not implemented");
	}
	/// <summary>
	/// Supprime le bat�ment
	/// </summary>
	public void Supprimer() {
		throw new System.NotImplementedException("Not implemented");
	}
	/// <summary>
	/// Acheter des ressources
	/// </summary>
	/// <param name="Ressource">La ressource � acheter</param>
	public void Acheter(Production ressource) {
		throw new System.NotImplementedException("Not implemented");
	}
	/// <summary>
	/// Permet de vendre des ressources
	/// </summary>
	/// <param name="Ressource">La ressource � vendre</param>
	public void Vendre(object ressource) {
		throw new System.NotImplementedException("Not implemented");
	}

}
