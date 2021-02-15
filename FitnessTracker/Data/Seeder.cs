using FitnessTracker.Helpers;
using FitnessTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FitnessTracker.Data
{
    public static class Seeder
    {
        public static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Moderator" },
                new Role { Id = 3, Name = "User" }
            );
        }

        public static void SeedGoals(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Goal>().HasData(
                new Goal { Id = 1, Name = "Redukcja tkanki tłuszczowej" },
                new Goal { Id = 2, Name = "Przybranie masy mięśniowej" },
                new Goal { Id = 3, Name = "Rekompozycja sylwetki" }
            );
        }

        public static void SeedCoach(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coach>().HasData(
                new Coach { Id = 1, Email = "coach_1@example.com", Name = "CoachName_1", Surname = "CoachSurname_1", Phone = "123456789", Description = "Hello, I'm a best coach ever", GoalId = 1 },
                new Coach { Id = 2, Email = "coach_2@example.com", Name = "CoachName_2", Surname = "CoachSurname_2", Phone = "987654321", Description = "Hi, I'm John Kowalski and with me, you'll be a strongman!", GoalId = 2 }
            );
        }

        public static void SeedExercise(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exercise>().HasData(
                new Exercise { Id = 1, Name = "Przysiad ze sztangą na plecach", Serie = 4, Powtorzenia = 8, GoalId = 1, Description = 
                     @"Stań w rozkroku na szerokość barków. Sztangę umieść na plecach i trzymaj ją szeroko. Zepnij mięśnie brzucha i
                     pośladków, a klatkę piersiową wysuń w przód. Palce stóp ułóż tak, by tworzyły kąt rozwarty. Plecy powinny być w
                     pozycji neutralnie prostej – ani się nie garb, ani też nie pogłębiaj lordozy. Z wdechem ugnij kolana i opuszczaj się do
                     momentu, gdy kolana i tyły ud będą w pozycji zbliżonej do kąta prostego. Przytrzymaj przez chwilę tę pozycję. Z
                     wydechem wstań, dbając o to, by nie doszło do przeprostu w kolanach." },
                new Exercise { Id = 2, Name = "Wykroki ze sztangielkami", Serie = 4, Powtorzenia = 8, GoalId = 3, Description = 
                    @"W pozycji stojącej trzymaj sztangielki wzdłuż ciała. Utrzymując stabilną i wyprostowaną sylwetkę weź wdech i
                    wykonaj krok do przodu. Kiedy udo nogi wykrocznej znajdzie się poziomo względem podłoża, odpychaj się od niego
                    prostując kolano i wykonując wydech w końcowej fazie ruchu. W kolejnych powtórzeniach zrób wykrok raz lewą, raz
                    prawą nogą." },
                new Exercise { Id = 3, Name = "Przysiad w szerokim rozkroku", Serie = 4, Powtorzenia = 8, GoalId = 3, Description =
                    @"Stań w szerokim rozkroku, stopy skieruj na zewnątrz. Ręce przenieś do przodu, napnij brzuch i wyprostuj plecy.
                    Podczas wydechu ugnij nogi w kolanach, a biodra opuść na wysokość kolan. Kolana pozostaw nad piętami.
                    Jednocześnie w wdechem prostuj nogi, napnij pośladki i wyprowadź biodra lekko do przodu."},
                new Exercise { Id = 4, Name = "Wyciskanie sztangielek w pozycji leżącej", Serie = 3, Powtorzenia = 12, GoalId = 3, Description = 
                    @"Połóż się na ławeczce, stopy podeprzyj na podłożu. Ręce ugnij w łokciach, sztangielki trzymaj nachwytem na
                    wysokości piersi. Weź wdech i wyprostuj ręce w pionie, obracając jednocześnie tak, aby dłonie znalazły się
                    naprzeciwko siebie. Następnie wykonaj skurcz izometryczny w części mostkowo-żebrowej mięśni piersiowych
                    większych, wykonaj wydech w końcowej fazie ruchu."},
                new Exercise { Id = 5, Name = "Brzuszki z uniesionymi nogami", Serie = 3, Powtorzenia = 25, GoalId = 3, Description =
                    @"Połóż się na plecach, nogi ugnij pod kątem prostym i unieś je tak, aby uda znajdowały się prostopadle do podłogi.
                    Spleć dłonie za głową, szeroko rozchylając łokcie. Wraz z wydechem unieś tułów kilka centymetrów nad ziemię.
                    Robiąc spięcie, staraj się „wciskać” odcinek lędźwiowy w podłogę i nie odrywaj go od podłoża przez całe ćwiczenie.
                    Robiąc wdech opuść barki. Pilnuj, aby pomiędzy udami a łydkami cały czas był kąt prosty" },
                new Exercise { Id = 6, Name = "Brzuszki skośne", Serie = 3, Powtorzenia = 25, GoalId = 3, Description = 
                    @"Połóż się na plecach z nogami ugiętymi w kolanach. Prawą nogę unieś do góry i oprzyj stopę o kolano. Załóż ręce za
                    głowę i unosząc tułów staraj się dotknąć lewym łokciem do przeciwległego kolana. Odcinek lędźwiowy kręgosłupa
                    pozostaje nieruchomy i dociśnięty do podłoża." },
                new Exercise { Id = 7, Name = "Plank(deska)", Serie = 2, Czas = 30, GoalId = 3, Description = 
                    @"Klęknij na podłodze. Oprzyj się na przedramionach, zginając ręce w łokciach pod kątem prostym. Barki powinny
                    znajdować się dokładnie nad łokciami. Opierając się na palcach stóp unieś tułów. Napnij mięśnie brzucha. Nie unoś
                    pośladków do góry ani nie wyginaj w dół odcinka lędźwiowego – pięty, biodra i ramiona powinny pozostać w prostej
                    linii. Wytrzymaj określoną ilość sekund." },
                new Exercise { Id = 8, Name = "Pompki ze stopami na podwyższeniu", Serie = 2, Powtorzenia = 12, GoalId = 1, Description = 
                    @"Przyjmij pozycję do pompki, opierając stopy na podwyższeniu. Może to być treningowa ławka, ale krzesło też spełni
                    swoje zadanie. Ręce rozstaw odrobinę szerzej niż na szerokość barków. Ustabilizuj sylwetkę i napnij mięśnie korpusu
                    i pośladki, aby zapobiec opadaniu bioder. Zginając łokcie, obniż pozycję, aż Twoja klatka znajdzie się tuż nad
                    podłożem. Dynamicznym ruchem wróć do startu." },
                new Exercise { Id = 9, Name = "Skakanka", Serie = 4, Czas = 120, GoalId = 1, Description = 
                    @"Na początek rozgrzej ramiona i stopy, poruszając nimi osobno. Rozpocznij trzymając skakankę w dłoniach ze
                    złączonymi stopami. Poruszaj skakanką w regularnym rytmie oraz skacz kiedy zbliża się do Twoich stóp. W celu
                    zwiększenia poziomu trudności ćwiczenia przy każdym skoku wykonuj double unders (podwójne skoki na skakance)." },
                new Exercise { Id = 10, Name = "Martwy ciąg", Serie = 2, Powtorzenia = 6, GoalId = 3, Description = 
                    @"Stajemy przodem do sztangi, w rozkroku na szerokość barków lub nieco szerszym, nogi lekko ugięte w kolanach, gryf
                    sztangi nad palcami stóp, sztangę chwytamy nachwytem, nieco szerzej niż barki. Klatka wypchnięta ku przodowi,
                    tułów wyprostowany, głowa lekko zadarta do góry. Ćwiczenie polega na unoszeniu sztangi w górę poprzez
                    prostowanie nóg i wyprost tułowia. Ruch kończymy przy pełnym wyproście tułowia- nie odchylamy go do tyłu. Nie
                    wolno również dopuszczać do tzw. ”kociego grzbietu”, czyli wygięcia pleców w łuk (szczególnie w dolnym odcinku).
                    Powrót do pozycji wyjściowej zaczynamy od lekkiego ugięcia nóg w kolanach, a następnie pochylamy
                    tułów(oczywiście cały czas jest on wyprostowany)robiąc skłon. Nie pochylamy się jednak zbyt nisko. Ruch
                    opuszczania ciężaru wolny i kontrolowany- sztanga nie uderza o podłogę, ale dotyka jej." },
                new Exercise { Id = 11, Name = "Pompki na poręczach", Serie = 3, Powtorzenia = 6, GoalId = 2, Description = 
                    @"Chwyć drążki chwytem neutralnym, czyli z czterema palcami skierowanymi na zewnątrz. Podkurcz nogi, by nie
                    dotykały podłoża. Ręce utrzymuj jak najbliżej tułowia. Wyprostuj ramiona w stawach łokciowych i wykonaj wydech.
                    Po wyprostowaniu ramion zaczerpnij głęboki wdech i zacznij opuszczać tułów do chwili, gdy odczujesz napięcie w
                    stawach barkowych –nie powinieneś opuszczać tułowia zbyt nisko, ponieważ może to spowodować kontuzję stawów
                    barkowych. Pamiętaj, aby łokcie trzymać blisko ciała. Po powrocie do pozycji startowej spróbuj powtórzyć ćwiczenie." },
                new Exercise { Id = 12, Name = "Pompki klasyczne", Serie = 4, Powtorzenia = 15, GoalId = 2, Description = 
                    @"Przyjmij pozycję podporu przodem. Dłonie ustaw na wysokości barków, nieco szerzej niż ich szerokość, palce dłoni
                    skieruj do przodu. Zablokuj ramiona w stawach łokciowych. Zachowaj naturalną krzywiznę kręgosłupa. Napnij
                    mięśnie brzucha i pośladki. Utrzymując prawidłową pozycję wyjściową, wykonaj dokładne, płynne i powolne
                    opuszczenie klatki piersiowej (bez dotykania podłoża) połączone z wdechem powietrza, a następnie dynamiczne
                    podnieś tułów wraz z wydechem powietrza." },
                new Exercise { Id = 13, Name = "Wyciskanie ekspandera zza pleców", Serie = 3, Powtorzenia = 12, GoalId = 2, Description = 
                    @"Do tego ćwiczenia potrzebny jest ekspander (zamiast niego można użyć elastycznej taśmy gumowej).
                    Stajemy w lekkim rozkroku, wyprostowani. Jedna ręka opuszczona wzdłuż ciała trzyma jeden uchwyt ekspandera.
                    Druga ręka zgięta w łokciu nad głową, trzyma za drugi koniec ekspandera przeciągniętego za plecami. W ćwiczeniu
                    pracuje staw łokciowy. Ręka nad głową prostuje się, naciągając ekspander i powraca do pozycji wyjściowej. Ułożenia
                    drugiej ręki nie zmienia się." },
                new Exercise { Id = 14, Name = "Nożyce pionowe i poziome", Serie = 5, Powtorzenia = 10, GoalId = 1, Description = 
                    @"Leżąc na plecach z wyprostowanymi nogami, unosimy je nad ziemię na ok. 40 cm i wykonujemy ruchy nożyc góradół lub na boki, 
                    starając się nie odrywać tułowia od podłogi. Część lędźwiowa mocno dociśnięta do podłoża. " },
                new Exercise { Id = 15, Name = "Krzesełko", Serie = 2, Czas = 30, GoalId = 3, Description = 
                    @"Oprzyj się o ścianę, nogi zegnij pod kątem 90°, ręce opuść wzdłuż tułowia, napnij brzuch. Plecy cały czas są
                    przyklejone do ściany, a nogi nie mogą przekroczyć linii utworzonej przez stopy na podłodze." },
                new Exercise { Id = 16, Name = " Przyciąganie kolan do klatki w zwisie na drążku", Serie = 4, Powtorzenia = 3, GoalId = 1, Description = 
                    @"Drążek złap nachwytem (kciuki na zewnątrz) na szerokość barków. Tułów w jednej linii z wyprostowanymi
                    kończynami dolnymi, miednica ustawiona neutralnie. Napinając mocno mięśnie brzucha, unieś kolana do klatki
                    piersiowej. Kontynuuj ruch, aż miednica uniesie się w górę. W najwyższym punkcie zatrzymaj ruch na ułamek
                    sekundy oraz wykonaj wydech. Następnie spokojnym kontrolowanym ruchem opuść kolana do pozycji początkowej." },
                new Exercise { Id = 17, Name = "Wspięcia na palcach stojąc z hantlą", Serie = 4, Powtorzenia = 12, GoalId = 2, Description = 
                    @"Przyjmij pozycję stojącą, plecy wyprostowane, ramię opuść wzdłuż tułowia, sztangielka w dłoni. Drugie ramię
                    podtrzymuje równowagę. Z pozycji, w której stopa jest mocno zadarta do góry, pięta skrajnie obniżona, palce
                    wskazują sufit, a łydka jest mocno rozciągnięta, odpychaj się od podwyższenia poprzez mocne wspięcie na palce i
                    napięcie łydek. Po krótkim utrzymaniu łydki w maksymalnym napięciu opuszczaj pięty poniżej poziomu palców,
                    ponownie rozciągając łydkę i wykonując wydech. W połowie serii zamień nogi." },
                new Exercise { Id = 18, Name = "Podciąganie sztangi pod brodę", Serie = 3, Powtorzenia = 10, GoalId = 1, Description = 
                    @"Stań wyprostowany, złap sztangę obiema rękami trochę szerzej niż na szerokość barków. Stopy w lekkim rozkroku,
                    plecy proste, napięty brzuch stabilizuje tułów. Kciuk skierowany do wewnątrz, w stronę tułowia. Wykonaj wydech i
                    podciągaj sztangę wzdłuż tułowia możliwie jak najwyżej, prowadząc łokcie w bok i do góry, pamiętając, aby
                    znajdowały się zawsze wyżej niż nadgarstki. Następnie opuszczaj sztangę, wykonując wdech." },
                new Exercise { Id = 19, Name = "Naprzemienne przyciąganie kolan do klatki w podporze przodem", Serie = 4, Powtorzenia = 12, GoalId = 1, Description = 
                    @"Przyjmij pozycję podporu przodem. Tułów wyprostowany, ręce ustaw prostopadle do osi ciała, dłonie w jednej linii,
                    ustawione równolegle do siebie, nieco szerzej niż rozstaw barków. Nogi wyprostowane, stopy złączone i mocno
                    zaparte o podłoże. Utrzymując prawidłową pozycję wyjściową, uruchamiając tłocznię brzuszną, przyciągaj
                    dynamicznie jedno z kolan do klatki piersiowej i wykonaj wydech powietrza. Cofnij nogę i jednocześnie przeskocz
                    drugą w stronę klatki piersiowej." },
                new Exercise { Id = 20, Name = "Martwy ciąg na jednej nodze", Serie = 2, Powtorzenia = 14, GoalId = 2, Description = 
                    @"Stań z równolegle rozstawionymi stopami pod biodrami, wyprostuj plecy. Robiąc wdech pochyl się do przodu w
                    biodrach, przenosząc ciężar na jedną nogę, która powinna być prosta, z lekkim ugięciem w kolanie. Druga noga
                    angażuje się i zaczyna się podnosić prosto za Tobą. Kontynuuj ruch do momentu wyczucia rozciągania w okolicach
                    mięśni dwugłowych ud. Powróć do pozycji wyjściowej, prostując tułów i wykonując wydech. W połowie serii zamień
                    nogi." },
                new Exercise { Id = 21, Name = "Wypychanie nogami na suwnicy", Serie = 4, Powtorzenia = 8, GoalId = 2, Description = 
                    @"Przyjmij pozycję siedzącą w siedzisku suwnicy. Plecy oraz głowa cały czas przylegają do oparcia maszyny. Dłonie z
                    boku na uchwytach maszyny. Nogi nieco szerzej od szerokości barków. Palce stóp skieruj lekko na zewnątrz. Wolno
                    opuszczaj ciężar mniej więcej do kąta prostego w stawie kolanowym, pamiętaj o wdechu powietrza. Wykonując
                    wydech, prostuj stawy kolanowe, wypychając ciężar, jednak bez przeprostu w kolanach." },
                new Exercise { Id = 22, Name = "Unoszenie bioder z uniesionymi palcami stóp", Serie = 2, Powtorzenia = 12, GoalId = 3, Description = 
                    @"Przyjmij pozycję leżącą na plecach. Nogi ugięte w kolanach, lekko rozstawione (nie szerzej niż rozstaw bioder), stopy
                    ułóż na wprost. Stopy zadarte w górę (palce uniesione, pięty wbite w podłogę). Wykonaj wdech do przepony,
                    utrzymując prawidłową pozycję, zacznij ruch od oderwania pośladków, pilnuj, aby równocześnie z nimi oderwały się
                    plecy. Przytrzymaj biodra w skrajnym górnym położeniu przez sekundę. Powoli wróć do pozycji wyjściowej,
                    obniżając biodra do podłoża, tak aby położyć je na podłogę, równocześnie wykonaj wydech." },
                new Exercise { Id = 23, Name = "Zginanie przedramion ze sztangą stojąc", Serie = 2, Powtorzenia = 6, GoalId = 2, Description = 
                    @"Przyjmij pozycję stojącą, plecy proste, sztanga trzymana podchwytem. Pełne ustabilizowanie łokci, które znajdują się
                    możliwie jak najbliżej tułowia. Dłonie rozstaw na szerokość nieco większą niż rozstaw barków. Przytrzymując
                    ramiona nieruchomo, wykonaj ugięcie ramion w łokciach ze sztangą do momentu szczytowego napięcia bicepsa, w
                    którym sztanga powinna znajdować się na wysokości barków. Utrzymuj stałe napięcie mięśni. Przytrzymaj
                    maksymalnie napięty biceps przez ułamek sekundy, w szczytowym momencie ruchu wykonaj wydech, a następnie
                    powolnym ruchem opuść przedramiona." },
                new Exercise { Id = 24, Name = "Wznosy ramion w leżeniu na brzuchu", Serie = 3, Powtorzenia = 12, GoalId = 1, Description = 
                    @"Przyjmij pozycję leżącą przodem (na brzuchu), ramiona wyprostuj i wyciągnij w przód. Mięśnie brzucha pozostaw w
                    napięciu. Leżąc w pozycji wyjściowej, przede wszystkim postaraj się złączyć łopatki. Utrzymując złączone łopatki,
                    weź oddech do przepony i unieś wyprostowane ręce. Zatrzymaj ruch w najwyższym położeniu przez 1 sekundę.
                    Kontrolując napięcie łopatek, opuść powoli ramiona, wróć do pozycji wyjściowej, tj. luźnej pozycji z
                    wyprostowanymi rękoma." },
                new Exercise { Id = 25, Name = "Przysiad bułgarski", Serie = 4, Powtorzenia = 8, GoalId = 3, Description = 
                    @"Stań około 50 cm przed stabilnym podparciem, stopy ustaw na szerokość bioder. Unieś jedną z nóg i umieść ją na
                    stabilnym podparciu. Zablokuj staw kolanowy i biodrowy, ręce swobodnie spoczywają wzdłuż tułowia lub są
                    podparte o biodra. Wzrok skierowany w przód, klatka piersiowa lekko wypchnięta w przód, lekko zgięte kolano nogi
                    zakrocznej. Weź oddech do przepony, z zachowaniem naturalnej krzywizny kręgosłupa rozpocznij ruch, wykonując
                    przysiad tak, aby kolana nogi z przodu nie wysuwać w przód. Zejdź do momentu, gdy twoje udo nogi wykrocznej
                    znajdzie się poniżej kolana lub do uczucia znaczącego rozciągania nogi zakrocznej. Stabilnie i płynnie wróć do
                    pozycji wyjściowej." },
                new Exercise { Id = 26, Name = "Rowerek", Serie = 3, Czas = 60, GoalId = 1, Description = 
                    @"Przyjmij pozycję leżącą na plecach. Ramiona ugnij za głową, tak, aby dłonie podtrzymywały lekko głowę. Nogi ugnij
                    w kolanach pod kątem 90 stopni. Odcinek lędźwiowy kręgosłupa dociśnij do podłogi. Wyprostuj prawą nogę,
                    jednocześnie przyciągnij lewe kolano do prawego łokcia i skręć tułów tak, aby możliwe było wykonanie ruchu.
                    Wykonaj wydech. Przez ułamek sekundy zatrzymaj ruch w maksymalnym napięciu mięśni brzucha. Wróć do pozycji
                    wyjściowej, wykonaj płynny wdech. Następnie wykonaj ruch na przeciwną stronę." },
                new Exercise { Id = 27, Name = "Głębokie przeskoki z nogi na nogę", Serie = 4, Powtorzenia = 8, GoalId = 1, Description = 
                    @"Przyjmij pozycję wykroczną. Plecy proste, łopatki ściągnięte, brzuch i pośladki mocno napięte. Z pozycji
                    wyprostowanej wykonaj wyskok w górę, po czym zamień nogi w powietrzu, każde lądowanie amortyzuj pracą nóg.
                    Po zamortyzowaniu przeskoku ponownie wyskocz w górę i zamień nogi miejscami. " },
                new Exercise { Id = 28, Name = "Przenoszenie sztangielki za głowę", Serie = 2, Powtorzenia = 10, GoalId = 2, Description = 
                    @"Przyjmij pozycję leżącą na ławeczce płaskiej. Stopy ustaw w lekkim rozkroku mocno zaparte o podłoże. Ramiona
                    unieś prostopadle do podłogi. Sztangielkę trzymaj oburącz, niech krążek spoczywa na wewnętrznej części dłoni, kciuk
                    i palec wskazujący obejmują uchwyt. Wykonaj wdech i powolnym, półkolistym ruchem opuść sztangielkę za głowę,
                    aż poczujesz silne rozciąganie w klatce piersiowej. Łokcie utrzymuj lekko zgięte. Następnie rozpocznij powrót do
                    pozycji wyjściowej, przenosząc sztangielkę na wysokość mostka oraz wykonując wydech. " },
                new Exercise { Id = 29, Name = "Floor press (wyciskanie sztangielek na ziemi)", Serie = 3, Powtorzenia = 8, GoalId = 2, Description = 
                    @"Przyjmij pozycję leżącą na podłodze, ugnij nogi. Unieś sztangielki. Ramiona ustaw pod kątem około 45 stopni
                    względem ciała. Wykonaj wdech do brzucha, zdecydowanym, kontrolowanym ruchem wyciśnij ciężar w górę poprzez
                    wyprost ramion, w końcowej fazie ruchu wykonaj wydech. W momencie prostowania ramion ze sztangielkami unikaj
                    przeprostu w łokciach. Spokojnym ruchem wróć do pozycji wyjściowej, wykonując wdech." },
                new Exercise { Id = 30, Name = "Uginanie nóg z hantlą w leżeniu", Serie = 4, Powtorzenia = 5, GoalId = 3, Description = 
                    @"Przyjmij pozycję leżącą przodem (na brzuchu), ramiona ugnij, aby były oparte na podłodze, a hantla pomiędzy
                    stopami. Mięśnie brzucha pozostaw w napięciu. Leżąc zaciśnij stopy tak, aby mieć pewność, że hantla nie wypadnie.
                    Utrzymując hantlę, rozpocznij ruch od zgięcia nóg w stawach kolanowych. Dochodząc do kąta prostego, zatrzymaj
                    ruch na ułamek sekundy. Kontrolując napięcie mięśni, opuść powoli podudzia, wróć do pozycji wyjściowej, ale
                    postaraj się nie tracić napięcia mięśniowego." },
                new Exercise { Id = 31, Name = "Unoszenie bioder na jednej nodze", Serie = 4, Powtorzenia = 6, GoalId = 2, Description = 
                    @"Połóż się na plecach. Nogi ugnij w kolanach, lekko rozstaw (nie szerzej niż biodra) ze stopami ułożonymi na wprost.
                    Unieś jedną z nóg. Wykonaj wdech do przepony, utrzymując prawidłową pozycję ciała, zacznij ruch od oderwania
                    pośladków, pilnując, aby równocześnie z nimi oderwały się plecy. Przytrzymaj biodra w skrajnym górnym położeniu
                    przez sekundę. Powolnym tempem wróć do pozycji wyjściowej, obniżając biodra tak, aby odłożyć je na podłogę. W
                    połowie serii zamień nogi." },
                new Exercise { Id = 32, Name = " Pompki tyłem na krześle", Serie = 2, Powtorzenia = 10, GoalId = 2, Description = 
                    @"Dłonie ułóż na podwyższeniu, stopy oprzyj o podłoże. Kończyny dolne wyprostuj, ciało ustaw w podporze tyłem,
                    tułów prostopadle do podłoża. Ramiona rozstaw na szerokość barków. Wykonaj wdech, a następnie ugnij
                    przedramiona, opuszczając ciało. W dolnej pozycji zatrzymaj ruch na ułamek sekundy, a następnie mocną pracą
                    wyprostną w łokciach unieś ciało i wróć do pozycji początkowej, w końcowej fazie ruchu wykonaj wydech." },
                new Exercise { Id = 33, Name = "Unoszenie nogi w górę w klęku podpartym", Serie = 4, Powtorzenia = 10, GoalId = 3, Description = 
                    @"Przyjmij pozycję klęku na macie z rękoma opartymi na podłodze prostopadle do tułowia. Ramiona ustaw równolegle
                    do siebie na szerokość barków. Kręgosłup prosty, ustaw równolegle do podłoża. Pracującą nogę wyprostuj. Wzrok
                    skieruj w przód. Weź wdech, zacznij unosić jedną nogę do momentu, aż udo znajdzie się w jednej linii z plecami i
                    będzie ustawione równolegle do podłoża. Mięsień pośladkowy powinien być teraz mocno napięty, utrzymaj tę pozycję
                    przez sekundę. Następnie powoli opuść nogę i wykonaj wydech. W połowie serii zamień nogi." },
                new Exercise { Id = 34, Name = "Naprzemianstronne przyciąganie kolan do łokci stojąc", Serie = 4, Powtorzenia = 6, GoalId = 1, Description = 
                    @"Stań prosto, tak, aby plecy były wyprostowane, nogi rozstawione na szerokość barków. Ramiona zgięte w łokciach,
                    skierowane w górę. Weź wdech i unieś lewe kolano w kierunku prawego łokcia. Podczas ruchu wykonaj lekki skręt
                    tułowia. W momencie zetknięcia kolana i łokcia wykonaj wydech. Zatrzymaj ruch na ułamek sekundy, następnie
                    wykonaj mocny wydech i wróć do pozycji początkowej. Wykonaj ruch drugą stroną." },
                new Exercise { Id = 35, Name = "Przyciąganie pięt do pośladków w leżeniu na plecach", Serie = 2, Powtorzenia = 6, GoalId = 3, Description = 
                    @"Przyjmij pozycję leżąca, nogi wyprostuj w kolanach, pięty oprzyj o ręcznik, kawałek materiału lub specjalne dyski do
                    slide'ów. Ramiona wyprostowane spoczywają wzdłuż tułowia. Wykonaj wdech, unieś lekko biodra, ugnij
                    jednocześnie kończyny dolne, próbując przyciągnąć pięty do pośladków. Postaraj się utrzymać maksymalne napięcie
                    mięśni dwugłowych ud, gdy nogi są ugięte w kolanach. Następnie wykonując wydech, wolno wróć do pozycji
                    wyjściowej, kontroluj ruch." },
                new Exercise { Id = 36, Name = "Wykroki w bok", Serie = 2, Powtorzenia = 15, GoalId = 1, Description = 
                    @"Stań prosto, nogi rozstaw na szerokość barków. Z pozycji wyprostowanej weź wdech i wykonaj obszerny wykrok w
                    bok, następnie obniż biodra, utrzymując plecy prostopadle do podłoża. Ciężar ciała spoczywa na całej stopie.
                    Odbijając się mocno od podłoża, powróć do pozycji wyjściowej; unosząc tułów, wykonaj wydech. Następnie wykonaj
                    wykrok nogą przeciwną." },
                new Exercise { Id = 37, Name = "Przysiad z wyskokiem w górę", Serie = 3, Powtorzenia = 10, GoalId = 3, Description = 
                    @"Przyjmij pozycję stojącą, plecy wyprostuj, ramiona skieruj w dół. Mięśnie brzucha oraz pośladki napnij, a łopatki
                    ściągnij. Stopy rozstaw mniej więcej na szerokości barków (palce stóp lekko na zewnątrz). Weź wdech, wykonaj
                    płynny i powolny przysiad (utrzymując naturalną krzywiznę kręgosłupa) do pozycji poniżej kąta prostego, ramiona
                    wysuń przed siebie. Z wydechem wykonaj dynamiczny wyprost kolan wraz z wyskokiem w górę i dynamiczną pracą
                    ramion. Opadając, amortyzuj skok, uginając kolana oraz schodząc równocześnie do pozycji przysiadu." },
                new Exercise { Id = 38, Name = "Prostowanie ramion z gryfem trzymanym podchwytem z wyciągu górnego", Serie = 3, Powtorzenia = 10, GoalId = 2, Description = 
                    @"Stań przodem do wyciągu. Chwyć oburącz za gryf podchwytem (kciuki skierowane na zewnątrz), sylwetkę wyprostuj.
                    Plecy proste, brzuch i pośladki napięte. Łokcie ułóż blisko tułowia, a ramiona prostopadle do ziemi. Weź wdech,
                    utrzymując prawidłową pozycję wyjściową; ramiona utrzymuj nieruchomo, łokcie blisko tułowia, prostuj ramiona,
                    ściągnij uchwyt wyciągu w dół i mocno napnij triceps aż do pełnego wyprostu ramion, w końcowej fazie ruchu
                    wykonaj wydech. W momencie maksymalnego napięcia zatrzymaj ruch na ułamek sekundy. Następnie powolnym i
                    kontrolowanym ruchem zacznij uginać ramię; rozciągając triceps, wróć do pozycji wyjściowej." },
                new Exercise { Id = 39, Name = "Wspięcia na palcach na suwnicy", Serie = 2, Powtorzenia = 8, GoalId = 2, Description = 
                    @"Usiądź na suwnicy do prostowania nóg. Przednia część stopy ustawiona na platformie, plecy oraz głowa dotykają
                    oparcia. Prostując kolana, zwalniaj maszynę. Z pozycji, w której stopa jest mocno zadarta, a łydka jest mocno
                    rozciągnięta, odpychaj się od platformy poprzez mocne wspięcie na palce i napięcie łydek. Po krótkim utrzymaniu
                    łydki w maksymalnym napięciu opuszczaj pięty poniżej poziomu palców, ponownie rozciągając łydkę." },
                new Exercise { Id = 40, Name = "Bieg bokserski", Serie = 4, Czas = 60, GoalId = 1, Description = 
                    @"Pozycja stojąca, tułów wyprostowany, stopy w lekkim rozkroku. Wykonuj bieg w miejscu połączony z
                    naprzemiennymi wyprostami ramion w przód (ciosami prostymi). Kiedy wykonujesz cios prawą ręką, lewa noga jest
                    ugięta, a kolano znajduje się wysoko z przodu (udo niemal równolegle do podłoża)." },
                new Exercise { Id = 41, Name = "Rewersy", Serie = 2, Powtorzenia = 5, GoalId = 3, Description = 
                    @"Połóż się na plecach, na macie. Ramiona ustaw prosto - wzdłuż tułowia. Kończyny dolne unieś z podłoża i ugnij w
                    kolanach. Odcinek lędźwiowy kręgosłupa dotyka maty. W leżeniu poprzez spięcie mięśni brzucha przyciągnij kolana
                    do klatki piersiowej. Ruch kontynuuj do momentu, aż biodra oderwą się od podłogi. Przez ułamek sekundy
                    przytrzymaj ruch w maksymalnym napięciu mięśni brzucha. Następnie powoli powróć do pozycji początkowej." },
                new Exercise { Id = 42, Name = "Dead bug – nogi proste", Serie = 4, Powtorzenia = 5, GoalId = 2, Description = 
                    @"Przyjmij pozycję leżącą na plecach. Nogi lekko ugnij w kolanach lub pozostaw proste, ramiona wyprostuj, skieruj
                    przed siebie. Spokojnym, kontrolowanym ruchem opuść przeciwne kończyny do podłogi, stale utrzymując napięcie.
                    Utrzymaj kończyny w dolnej pozycji przez sekundę. Zdecydowanym ruchem wróć do pozycji wyjściowej." },
                new Exercise { Id = 43, Name = "Naprzemienne sięganie do kostek leżąc", Serie = 2, Powtorzenia = 12, GoalId = 1, Description = 
                    @"Pozycja leżąca, kończyny dolne ugięte, stopy na podłodze rozstawione na szerokość barków. Ramiona wyprostuj, aby
                    spoczywały wzdłuż tułowia. Broda przyklejona do klatki piersiowej. Odrywając łopatki od maty, zegnij swój tułów w
                    bok i sięgnij lewą dłonią do lewej kostki. Wykonaj wydech w tej części ruchu. Wróć do pozycji wyjściowej,
                    wykonując wdech. Łopatki wraz z górną częścią pleców pozostaw w powietrzu. Wykonaj ruch w prawą stronę.
                    Pamiętaj, że obustronne sięgnięcie w stronę kostek oznacza jedno powtórzenie." },
                new Exercise { Id = 44, Name = "Wyprosty kolan na maszynie siedząc", Serie = 6, Powtorzenia = 8, GoalId = 2, Description = 
                    @"Pozycja siedząca na siedzisku. Dłonie z boku na uchwytach maszyny, proste plecy. Wykonaj dynamiczny wyprost
                    kolan, jednak bez przeprostu (stałe napięcie mięśnia). Ruch wykonuj wraz z wydechem powietrza. Wraz z wdechem
                    uginaj powolnym ruchem kolana do pozycji wyjściowej." },
                new Exercise { Id = 45, Name = "Wiosłowanie w oparciu o kolano", Serie = 2, Powtorzenia = 10, GoalId = 2, Description = 
                    @"Pozycja wykroczno-rozkroczna (noga zakroczna w tył i lekko na zewnątrz). Łokieć oparty na nodze z przodu. Plecy
                    proste, hantla w dłoni, ramię skierowane w dół. Wykonaj wdech, a następnie przyciągnij sztangielkę pionowo w górę,
                    kierując ją w stronę biodra, jednoczenie maksymalnie zbliżając łopatkę do kręgosłupa i w dół. Kontynuuj ruch do
                    momentu, w którym sztangielka będzie znajdowała się na wysokości biodra, a plecy uzyskają szczytowe napięcie.
                    Utrzymaj napięcie przez ułamek sekundy. Ruch opuszczania wykonaj zdecydowanie wolniej niż podnoszenia, zrób
                    wydech. W połowie serii zamień ręce." },
                new Exercise { Id = 46, Name = "Uginanie ramion z hantlami z rotacją", Serie = 4, Powtorzenia = 4, GoalId = 3, Description = 
                    @"Pozycja stojąca. Ramiona ułożone wzdłuż ciała, brzuch i pośladki napięte. Hantle w dłoniach w pozycji neutralnej –
                    kciuki skierowane w przód. Utrzymując prawidłową pozycję wyjściową, wykonaj wdech. Rozpocznij uginanie z
                    jednoczesną rotacją zewnętrzną (supinacją), aby finalnie dłoń była skierowana kciukiem na zewnątrz. Zatrzymaj ruch
                    na ułamek sekundy w pozycji końcowej. Z wydechem wykonaj ruch opuszczania znacznie wolniej niż podnoszenia,
                    wracając do neutralnej pozycji ramion." },
                new Exercise { Id = 47, Name = "Prostowanie ramion z gumą", Serie = 3, Powtorzenia = 7, GoalId = 2, Description = 
                    @"Stań przodem do gumy oporowej zaczepionej wyżej. Sylwetka wyprostowana, nieznacznie pochylona. Łokcie
                    znajdują się blisko tułowia. Weź wdech i zacznij prostować ramiona w stawach łokciowych. Podczas ruchu utrzymuj
                    ciało nieruchomo, łopatki mocno ściągnięte, kontynuuj ruch do pełnego wyprostu w stawie łokciowym. W momencie
                    maksymalnego napięcia zatrzymaj ruch na ułamek sekundy, a następnie zacznij odkładać ciężar, wydychając
                    powietrze." },
                new Exercise { Id = 48, Name = "Przyciąganie kolan pod klatkę na piłce", Serie = 2, Powtorzenia = 8, GoalId = 1, Description = 
                    @"Przyjmij pozycję podporową przodem z nogami ustawionymi na piłce. Z pozycji wyjściowej rozpocznij wykonywanie
                    unoszenia bioder. Równocześnie uginaj kolana, kierując je do klatki piersiowej. W momencie maksymalnego spięcia
                    utrzymaj pozycję przez ułamek sekundy. Spokojnym, kontrolowanym ruchem wróć do pozycji wyjściowej." },
                new Exercise { Id = 49, Name = "Mostek biodrowy z hantlą", Serie = 3, Powtorzenia = 5, GoalId = 3, Description = 
                    @"Połóż się na plecach. Nogi ugnij w kolanach, lekko rozstaw (nie szerzej niż na szerokość bioder), stopy ułóż na
                    wprost, hantlę na biodrach. Wykonaj wdech przeponą, utrzymując prawidłową pozycję ciała, zacznij ruch od
                    oderwania pośladków, pilnuj przy tym, aby równocześnie z nimi oderwały się plecy. Przytrzymaj biodra w górze
                    przez sekundę. Wolno wróć do pozycji wyjściowej, obniżaj biodra, aż znajdą się na podłodze, równocześnie wykonaj
                    wydech." },
                new Exercise { Id = 50, Name = "Russian twist", Serie = 2, Powtorzenia = 8, GoalId = 3, Description = 
                    @"Dobierz odpowiadający ciężarem kettlebell (możesz użyć butelkę z wodą) następnie usiądź na podłodze. Chwyć
                    odważnik kettlebell i trzymaj go przed klatką piersiową. Następnie nieznacznie odchyl tułów do tyłu, złącz nogi i
                    ugnij je w kolanach. Lekko unieś stopy, aby dojść do pozycji siadu równoważnego. Klatka piersiowa wypchnięta do
                    przodu, łopatki ściągnięte, brzuch i pośladki napięte. Plecy w odcinku lędźwiowym utrzymaj proste. Pozostając w
                    siadzie równoważnym, skręć powoli tułów i przenieś odważnik kettlebell w kierunku prawej strony biodra. Przez
                    ułamek sekundy zatrzymaj ruch w maksymalnym napięciu mięśni brzucha. Następnie wykonaj ruch w stronę
                    przeciwną." }
            );
        }

        public static void SeedTraining(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Training>().HasData(
                new Training { Id = 1, IsPublic = true, Name = "Trening_Publiczny_1" },
                new Training { Id = 2, IsPublic = false, Name = "Trening_Prywatny_1" },
                new Training { Id = 3, IsPublic = false, Name = "Trening_Prywatny_2" },
                new Training { Id = 4, IsPublic = true, Name = "Trening_Publiczny_2" }
            );

            modelBuilder.Entity<TrainingExercise>().HasData(
                new TrainingExercise { ExerciseId = 1, TrainingId = 1 },
                new TrainingExercise { ExerciseId = 2, TrainingId = 1 },
                new TrainingExercise { ExerciseId = 3, TrainingId = 1 },
                new TrainingExercise { ExerciseId = 4, TrainingId = 1 },
                new TrainingExercise { ExerciseId = 1, TrainingId = 2 },
                new TrainingExercise { ExerciseId = 5, TrainingId = 2 },
                new TrainingExercise { ExerciseId = 6, TrainingId = 2 },
                new TrainingExercise { ExerciseId = 1, TrainingId = 3 },
                new TrainingExercise { ExerciseId = 2, TrainingId = 3 },
                new TrainingExercise { ExerciseId = 6, TrainingId = 3 },
                new TrainingExercise { ExerciseId = 7, TrainingId = 3 },
                new TrainingExercise { ExerciseId = 4, TrainingId = 4 },
                new TrainingExercise { ExerciseId = 3, TrainingId = 4 },
                new TrainingExercise { ExerciseId = 2, TrainingId = 4 },
                new TrainingExercise { ExerciseId = 5, TrainingId = 4 }
            );

            modelBuilder.Entity<UserTraining>().HasData(
                new UserTraining { UserId = 1, TrainingId = 1, Favourite = false },
                new UserTraining { UserId = 1, TrainingId = 2, Favourite = false },
                new UserTraining { UserId = 1, TrainingId = 3, Favourite = true },
                new UserTraining { UserId = 2, TrainingId = 3, Favourite = false },
                new UserTraining { UserId = 3, TrainingId = 3, Favourite = false },
                new UserTraining { UserId = 3, TrainingId = 4, Favourite = true }
            );

            modelBuilder.Entity<TrainingHistory>().HasData(
                new TrainingHistory { Id = 1, UserId = 1, TrainingId = 3, Date = DateTime.Parse("2020-12-05") },
                new TrainingHistory { Id = 2, UserId = 2, TrainingId = 3, Date = DateTime.Parse("2020-12-03") },
                new TrainingHistory { Id = 3, UserId = 2, TrainingId = 3, Date = DateTime.Parse("2020-12-04") },
                new TrainingHistory { Id = 4, UserId = 1, TrainingId = 1, Date = DateTime.Parse("2020-12-03") },
                new TrainingHistory { Id = 5, UserId = 1, TrainingId = 2, Date = DateTime.Parse("2020-12-04") }
            );
        }

        public static void SeedHistory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExerciseHistory>().HasData(
                new ExerciseHistory { Id = 1, UserId = 1, ExerciseId = 1, Date = DateTime.Parse("2020-12-03") },
                new ExerciseHistory { Id = 2, UserId = 1, ExerciseId = 2, Date = DateTime.Parse("2020-12-03") },
                new ExerciseHistory { Id = 3, UserId = 1, ExerciseId = 1, Date = DateTime.Parse("2020-12-04") },
                new ExerciseHistory { Id = 4, UserId = 1, ExerciseId = 2, Date = DateTime.Parse("2020-12-04") }
            );

            modelBuilder.Entity<ExerciseHistoryStats>().HasData(
                new ExerciseHistoryStats { Id = 1, Serie = 1, Powtorzenia = 1, ExerciseHistoryId = 1 },
                new ExerciseHistoryStats { Id = 2, Serie = 2, Powtorzenia = 4, ExerciseHistoryId = 1 },
                new ExerciseHistoryStats { Id = 3, Serie = 3, Powtorzenia = 6, ExerciseHistoryId = 1 },
                new ExerciseHistoryStats { Id = 4, Serie = 1, Powtorzenia = 3, ExerciseHistoryId = 2 },
                new ExerciseHistoryStats { Id = 5, Serie = 2, Powtorzenia = 6, ExerciseHistoryId = 2 },
                new ExerciseHistoryStats { Id = 6, Serie = 3, Powtorzenia = 8, ExerciseHistoryId = 2 },
                new ExerciseHistoryStats { Id = 7, Serie = 7, Powtorzenia = 9, ExerciseHistoryId = 3 },
                new ExerciseHistoryStats { Id = 8, Serie = 2, Powtorzenia = 2, ExerciseHistoryId = 3 },
                new ExerciseHistoryStats { Id = 9, Serie = 8, Powtorzenia = 1, ExerciseHistoryId = 4 },
                new ExerciseHistoryStats { Id = 10, Serie = 4, Powtorzenia = 5, ExerciseHistoryId = 4 }
            );
        }

        public static void SeedUsers(ModelBuilder modelBuilder, IAuthHelper authHelper)
        {
            authHelper.CreatePasswordHash("Password#2!", out byte[] passwordHash, out byte[] passwordSalt);

            User adminUser = new User
            {
                Id = 1,
                Email = "admin@gmail.com",
                Name = "Admin Name",
                Surname = "Admin Surname",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = 1,
                GoalId = 2
            };

            User moderatorUser = new User
            {
                Id = 2,
                Email = "moderator@gmail.com",
                Name = "Moderator Name",
                Surname = "Moderator Surname",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = 2,
                GoalId = 3
            };

            User user = new User
            {
                Id = 3,
                Email = "user@gmail.com",
                Name = "User Name",
                Surname = "User Surname",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = 3,
                GoalId = 1
            };

            modelBuilder.Entity<User>().HasData(
                adminUser,
                moderatorUser,
                user
            );
        }
    }
}
