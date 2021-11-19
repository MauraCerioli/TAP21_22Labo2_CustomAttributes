# TAP21_22Labo2_CustomAttributes
Cooperative solution of TAP Labo2
The labo text follows (in Italian, sorry)

<h1><span style="font-size: 1.5em;">Custom-attribute e reflection</span></h1>
<div class="box generalbox center clearfix p-y-1">
<div class="no-overflow">
<div id="yui_3_15_0_2_1445286733931_523" class="box generalbox center clearfix">
<div id="yui_3_15_0_2_1445286733931_522" class="no-overflow">
<p>L'esercizio proposto è articolato in una versione base ed alcuni miglioramenti, seguendo i principi dello sviluppo incrementale.</p>
<p>L'intero laboratorio dovrebbe poter essere svolto, da uno studente medio che abbia riguardato il materiale su C# prima del laboratorio, nelle due ore previste.<br />Chiaramente se durante il laboratorio dovete rivedere anche il materiale sarete più lenti e potreste doverlo completare "a casa".<br />Se però non riuscite a completare in questo tempo neppure la prima parte, vuol dire che la vostra preparazione di programmazione di base è carente e dovreste fare esercizio.</p>
<h2>Versione base</h2>
<p>Creare una solution con:</p>
<ol id="yui_3_15_0_2_1445286733931_521">
<li>Una console application Executer</li>
<li>Una libreria di classi MyAttribute</li>
<li id="yui_3_15_0_2_1445286733931_520">Una libreria di classi MyLibrary</li>
</ol>
<p>(potete partire creando un progetto e la sua solution, poi aggiungere gli altri due progetti con "Add new project" dal menù contestuale della solution)</p>
<p>In MyAttribute definite un custom-attribute ExecuteMe, con un numero arbitrario di parametri, che possa essere associato, anche più volte, a metodi (e solo a loro).</p>
<p>In MyLibrary aggiungete un reference al progetto MyAttribute (dal menù contestuale del progetto, "Add Reference"); questo vi permetterà di usare il custom-attribute [ExecuteMe] dentro alla libreria MyLibrary.</p>
<p>In MyLibrary create delle classi pubbliche con costruttori pubblici e senza parametri. Aggiungete, a queste classi, dei metodi pubblici annotati con [ExecuteMe].</p>
<p>Per esempio:</p>
<pre>public class Foo {
        [ExecuteMe]
        public void M1() {
            Console.WriteLine("M1");
        }

        [ExecuteMe(45)]
        [ExecuteMe(0)]
        [ExecuteMe(3)]
        public void M2(int a) {
            Console.WriteLine("M2 a={0}", a);
        }

        [ExecuteMe("hello", "reflection")]
        public void M3(string s1, string s2) {
            Console.WriteLine("M3 s1={0} s2={1}", s1, s2);
        }
    }
</pre>
<p>Verificate che l'attributo [ExecuteMe] possa essere applicato solo a metodi (applicandolo a campi/classi/ecc e provando a compilare...)</p>
<p>A Executer aggiungete i reference agli altri due progetti.<br />Nel suo Main, tramite reflection, caricate la DLL di MyLibrary.</p>
<p>Per esempio, potreste fare così:<br />var a = Assembly.LoadFrom("MyLibrary.dll");<br />foreach (var type in a.GetTypes())<br />if (type.IsClass)<br />Console.WriteLine(type.FullName);<br />Console.ReadLine();<br /><br />Notate che, al posto di MyLibrary.dll ci potrebbe essere il nome di una DLL qualsiasi (la console application NON ha davvero bisogno del reference al progetto MyLibrary, l'abbiamo aggiunto solo per fare in modo che la DLL di MyLibrary venisse copiata automaticamente nella stessa directory dell'eseguibile).</p>
<p>Aggiungete al Main del codice che, sfruttando la reflection, invochi tutti i metodi pubblici (di tutte le classi trovate nella DLL) che siano stati annotati con [ExecuteMe], passando come argomenti gli argomenti dell'annotazione.<br />Facendo riferimento alla classe Foo mostrata sopra, M1 dovrà essere invocato senza parametri, il metodo M2 dovrà essere invocato tre volte (con argomenti: 3, 0 e 45) e M3 dovrà essere invocato una volta con argomenti s1="hello" e s2="reflection".</p>
<p>Notate che per invocare i metodi d'istanza dovrete prima creare degli oggetti (motivo per cui abbiamo detto di inserire classi con costruttori pubblici senza argomenti)...Hint: classe Activator...</p>
<p>In questa prima release assumete che non ci siano errori nelle annotazioni ovvero che numero e tipi degli argomenti di ciascuna annotazione corrispondano a quanto necessario per l'invocazione del metodo annotato.<br />Assumete anche che i parametri dei metodi in MyLibrary siano tutti per valore, necessari e non "params".</p>
<h2>Seconda release</h2>
<p>Perfezionare il progetto precedente sotto i seguenti aspetti.</p>
<h3>Corretto riferimento a MyLibrary</h3>
<p>Eliminare il riferimento al progetto MyLibrary nel progetto Executer, che logicamente non dovrebbe esserci ed è stato introdotto solo per semplificare il caricamento della dll, e modificare il codice in modo che continui a funzionare come prima.</p>
<p>Provate a modificare MyLibrary aggiungendo un metodo annotato con [ExecuteMe] e fare F5 (dell'Executer), sia prima che dopo aver eliminato il riferimento (e fatto le conseguenti modifiche al codice).<br />Confrontate i risultati ottenuti nei due casi. Ci sono differenze? quali e perché?</p>
<h3>Parametri di ExecuteMe</h3>
<p>Provate a sperimentare con quali valori e quali tipi sono ammissibili come argomenti per l'annotazione. <br />Riflettete sul perché delle restrizioni.</p>
<h3>Gestione errori: assenza del costruttore di default</h3>
<p>Gestire il caso in cui in MyLibrary siano presenti classi senza il costruttore di default in modo che il fallimento sia "graceful" e l'esecuzione prosegua con l'analisi delle classi successive.</p>
<p>Per verificare di aver correttamente gestito questo caso, aggiungete alla vostra MyLibrary una classe pubblica senza costruttore di default e con almeno un metodo pubblico annotato con [ExecuteMe], seguita da una ulteriore classe pubblica con costruttore di default e almeno un metodo pubblico annotato con [ExecuteMe]</p>
<h3>Gestione errori: parametri sbagliati</h3>
<p>Gestire il caso in cui un'annotazione con [ExecuteMe] fornisca argomenti di invocazione non adeguati come numero o come tipo, fornendo un messaggio di errore informativo e proseguendo con l'analisi dei metodi e delle classi successive.</p>
<p>Per verificare di aver correttamente gestito questo caso, aggiungete ad una classe pubblica, con costruttore di default, un metodo pubblico con un argomento di tipo int annotato con [ExecuteMe("tre")], seguito da un ulteriore metodo pubblico M1024 senza argomenti annotato con [ExecuteMe] e verificate che la chiamata di M1024 sia correttamente eseguita.</p>
<p>Provate a gestire anche il caso in cui il metodo si aspetti parametri per riferimento.</p>
<h2>Per chi ancora avesse tempo e si annoiasse</h2>
<ol>
<li>
<p>Come si potrebbe gestire il caso di costruttori non di default? <br />Valutate due possibili soluzioni:</p>
<ol>
<li>Introdurre un (ulteriore) custom attribute per annotare i costruttori con valori dei parametri, da usare in assenza del costruttore di default.<br />Sotto quali aspetti questo è diverso da avere parametri con valore di default nel costruttore?</li>
<li>Chiedere all'utente di fornire i valori dei parametri per il costruttore.</li>
</ol></li>
<li>
<p>Riflettete su come si può gestire il caso di parametri opzionali e "params". Considerate le interazioni fra i due casi e i vari "corner case" dovuti alla presenza di più parametri opzionali nella dichiarazione, di cui solo alcuni esplicitamente presenti nell'annotazione.<br />Come punto di partenza considerate che il compilatore ammette:</p>
<ul>
<li>"params" solo come ultimo parametro;</li>
<li>parametri opzionali solo come ultimi (ad eccezione, se presente, di un eventuale "params")</li>
<li>omissione di un argomento corrispondente ad un parametro opzionale "intermedio" solo se i successivi argomenti sono indicati nominalmente, cioè se abbiamo dichiarato<br />
<pre>public void Abc(int a, int b = 1, string c = "2fghd", int d = 3,double e=5.5)
</pre>
la chiamata
<pre>Abc(0,c:"puffo", e:2.3);
</pre>
è corretta, mentre la seguente non lo è
<pre>Abc(0,"puffo", 2.3);
</pre>
nonostante il fatto che i tipi permetterebbero di capire la corrispondenza fra argomenti e parametri.</li>
</ul>
</li>
</ol></div>
</div>
</div>
</div>
