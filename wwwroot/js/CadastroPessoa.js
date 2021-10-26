function ControleAba(IdAbaVisivel)
{
    var Abas = document.getElementsByClassName("aba");
    for (i = 0; i < Abas.length; i++)
    {
        DesaparecerAba(Abas[i].id);
    }
    AparecerAba(IdAbaVisivel);
}

function AparecerAba(IdAba)
{
    document.getElementById(IdAba).hidden = false;
}

function DesaparecerAba(IdAba)
{
    document.getElementById(IdAba).hidden = "true";
}

function LimparAlerta()
{
    var Alerta = document.getElementsByName("alerta");
    for (i = 0; i < Alerta.length; i++)
    {
        Alerta[i].removeChild();    
    }
}
