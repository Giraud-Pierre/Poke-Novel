using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialogues : ScriptableObject
{
    //Tous les dialogues qui peuvent être lancés dans le jeu
    public List<string> Mom = new List<string>
    {
        "Bonjour mon chéri, il enfin temps pour toi d'aller chercher ton premier pokemon",
        "Le professeur Pin t'attend dans son laboratoire. Ne le fais pas attendre !"
    };
    public List<string> RandomAnnoyingDude = new List<string>
    {
        "Fais attention ! Le monde est plein de pokemons sauvages!",
        "Si tu n'a pas de Pokemon pour te défendre et que tu te fait attaquer,",
        "cela pourrait mal tourner pour toi !"
    };
    public List<string> ProfessorPine = new List<string>
    {
        "Bienvenue Red, il est grand temps que tu obtienne ton premier pokemon,",
        "Une grand aventure t'attend ! Choisis un des trois pokemons que voici."
    };
    public List<string> RivalBattle = new List<string>
    {
        "He bien, il t'en aura fallu du temps, minable!",
        "Maintenant que tu as enfin ton pokemon, voyons-voir lequel de nos deux pokemon est le plus fort !"
    };
    public List<string> RivalVictory = new List<string>
    {
        "Nooon ! C'est impossible ! Comment as-tu pu gagner ?",
        "Ce n'est que partie remise ! Je vais entraîner mon pokemon d'arrache-pied et en capturer plein d'autre",
        "On verra bien qui de nous deux est le plus fort, minable !"
    };
    public List<string> RivalDefeat = new List<string>
    {
        "Pokemon maigrichon pour un dresseur de pacotille, vous allez bien ensemble.",
        "Ne t'en veux pas trop, c'est probablement juste moi qui est trop fort.",
        "Allez, à plus minable !"
    };
    public List<string> RivalMomBeforeLab = new List<string>
    {
        "Salut Red ! Tu cherche Blue ? Il est déjà parti au laboratoire du professeur !",
        "Si tu te dépêche, tu peux peut-être encore le rattraper."
    };
    public List<string> RivalMomAfterLab = new List<string>
    {
        "Salut Red ! Tu cherche Blue ? Il a déjà quitté le village.",
        "Il est toujours très pressé mais prend ton temps, ce n'est pas une course."
    };
    public List<string> End = new List<string>
    {
        "Une aventure fantastique vous attend dans le monde de pokemon",
        "To be continued"
    };
}