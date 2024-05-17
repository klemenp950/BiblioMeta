# BiblioMeta
Študenta: Klemen Parkelj in Klavdija Jakše. 
Naslov: BiblioMeta
## Opombe
Ker sva oba študenta porabila zastonj credite za Azure ki jih dobimo od fakultete je baza lokalno nameščena v Dockerju. Brez baze sistem ne deluje.
## Opis:
Za založbo knjig bova naredila informacijski sistem v katerega vnesemo besedilo knjige, ta pa bo povedal koliko vrstic bo imela, koliko strani bo potrebovala itd. 
Sistem bo podpiral prijavo vsaj treh vrst uporabnikov. Imenovali jih bomo SuperAdmin, Admin in User. Gre za nivoje zaposlenih v podjetju. User bo imel najmanj pravic in bo lahko le dodajal knjige. Ne bo jih moral spreminjati ali brisati iz sistema. Admin bo lahko dodajal še nove avtorje knjig, za katere bo založba tiskala knjige. Poleg tega lahko ureja že vnesene knjige. SuperAdmin pa bo lahko knjige tudi brisal in in dodajal novo kategorijo knjig za tiskanje. Kategorijo knjige in avtorja bo lahko uporabnik izbral iz spustnega menija. Sistem bo onemogočil Userju in Adminu dostop do stvari ki jih ne sme spreminjati. 
Ko v sistem vnesemo besedilo, bo ta preštel vrstice in besede ter na podlagi tega predvidel koliko črnila in strani bo potrebno za tisk. Možno je tudi da bo sistem tudi predvidel ceno tiska posamezne knjige, in v primeru vnosa števila knjig ki jih želimo natisniti bo predvidel ceno celotnega natiska.
Sistem bo omogočal dvo-faktorsko avtentikacijo s pomočjo aplikacije ki generira kodo ki jo vnesemo po vnosu gesla. 
Poleg tega bo informacijski sistem podpiral dostop do podatkov preko API-ja, ki pa ne bo dovoljen vsem. Dostop do njega bo mogoč samo z geslom. To omogoča enostavnejšo razširljivost sistema v prihodnosti, v idealnem scenariju bo sistem prišel z mobilno aplikacijo, ki bi dovolila uporabniku ki nima dostopa do računalnika pogled podatkov ki so trenutno v sistemu. 
Sistem bo imel tudi funkcijo ki omogoča generacijo pdf poročila knjig trenutno v sistemu, pripravljeno za tisk. 

## Funkcionalnosti sistema:
- Prijava v sistem
-	Prijava za nadzornika
-	Registracija novih uporabnikov
-	Spremljanje metapodatkov o knjigah
-	Dodajanje novih knjig
-	Avtomatsko izpolnjevanje  podatkov kot je število znakov, vrstic in strani
-	Izdaja izpisa stanja v natisljivi obliki
-	Two-Factor Authentication
-	API

## Tok dogodkov:
-	Uporabnik se prijavi v sistem
-	Uporabnik pregleda trenutne knjige
-	Uporabnik doda novo knjigo
-	Uporabnik lahko pregleda podrobnosti dodane ali katerekoli ostale knjige
## Alternativni tok dogodkov:
-	Uporabnik se prijavi v sistem
-	Poskuša dodati avtorja
-	Sistem preveri njegove pravice
-	Sistem ne dovoli dodajanje avtorja ker nima pravic
