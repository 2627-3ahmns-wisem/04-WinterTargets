# Frosty Targets – 3AHMNS

Unity-Version: **6000.3.24f1**. Projekt: Frosty Targets.

## Projekt öffnen
1. Unity Hub öffnen, **Add project from disk** wählen und diesen Ordner auswählen.
2. Unity **6000.3.24f1** verwenden und den ersten Paketimport abwarten.
3. Szene `Assets/MyGame/Scenes/ShootingScene.unity` öffnen, **Play** drücken.
4. `Assets/MyGame/Scripts/GameManager.cs` und `Target.cs` untersuchen.

## Hinweis bei `UnityEngine.UI` / `Text`-Fehlern
Das Paket **Unity UI (uGUI)** ist unter `Packages/manifest.json` als `com.unity.ugui` eingetragen. Falls trotzdem CS0234 oder CS0246 erscheint: `Window > Package Management > Package Manager` öffnen und unter **In Project** prüfen, ob Unity UI geladen ist. Im Paketmanager **Refresh/Resolve** ausführen; gegebenenfalls Unity schließen und den Ordner `Library` löschen, dann erneut öffnen. Nicht die `using UnityEngine.UI`-Zeile entfernen: sie wird für die vorhandene Punkteanzeige gebraucht.

## Unterricht
Das vorhandene Spawning, der Score und der Sieg bei zehn Treffern sind **Ausgangscode**. Schüler:innen erweitern diese Mechaniken anhand der GitHub-Issues. Der Erstimport ist eine Ausgangsversion; keine Aufgaben sind vorweg implementiert.


**1. Auftrag**

*Ablauf*

Sobald man das Spiel starten, beginnen Zielscheiben sich zufällig auf dem Bildschirm zu verteilen. Sobald der Player mit einem Linksklick auf eine der Zielscheiben trifft, erhöht sich der Score um +1. Bei 10 Punkten wird das Gewinn-Bild eingeblendet und es werden keine neuen Ziele mehr erzeugt.

**Bereits implementierte Funktionen**

*GameManager*

Start() -> starten mit InvokeRepeating("Spawn", 1f, 1f) das Erzeugen der Ziele (erstes Ziel nach einer Sekunde, danach jede Sekunde)

Update() -> läuft jeden Frame. Wenn 'win' true ist, wird das Spawnen mit CancelInvoke gestoppt. Bei jedem Linksklick wird außerdem der Schuss-Sound abgespielt (durch eine Audiosource)

Spawn() -> berechnet eine zufällige Position und erzeugt mit Instantiate eine Kopie des Ziel-Prefabs als 'Kind' von parentOfTargets. Weil das Ziel ein Kind-Objekt ist, wird localPosition statt position verwendet. 

IncrementScore() -> erhöht den Punktestand um 1 und schreibt ihn in scoreText ab. Ab 10 Punkten wird win auf true gesetzt und winSprite angezeigt.

*Target*

Start() -> sucht den GameManager in der Szene und speichert die Referenz. Zerstört das Ziel nach 2 Sekunden, falls es nicht getroffen wird.

OnMouseDown() -> wird beim Anklicken des Ziels aufgerufen (braucht einen Collider). Ruft IncrementScore() auf und zerstört das Ziel sofort.


*Geplante Änderungen* 

-> Zieltypen unterschiedlichen Punktewerten
-> Zieltypen mit Wahrscheinlichkeiten
-> Bewegte Ziele
-> Countdown und neue Spielende-Regeln
-> Winterliches Design mit Sound und Trefferanimation
-> Spiel testen, Fehler beheben und dokumentieren





## Assets und Quellen

Background | https://at.pinterest.com/pin/712694709809470902/


Schneemann-Snoopy | https://at.pinterest.com/pin/389772542771292210/


Eisblock-Snoopy | https://at.pinterest.com/pin/120471358769832898/


Geschenk-Snoopy | https://at.pinterest.com/pin/856317316687637699/


Treffer-Sound | https://pixabay.com/de/sound-effects/search/ice%20cube/


Fail-Sound | https://pixabay.com/de/sound-effects/search/fail/

