# MovilesTP1_JuanRodriguezGiles

EXE: https://drive.google.com/file/d/1FQTvtaVIRQjFfLr5ZwpLPjl5r7Q6Z3A8/view?usp=sharing

APK: https://drive.google.com/file/d/1zwVf2HyAUYY2SLosfzWVftrc-_c_lovk/view?usp=sharing

Patrones de diseño:

1. Singleton:
Aplicado en GameManager. Se aplica el uso de singleton para tener una sola instancia del GameManager. Esto permite que otras clases puedan acceder a la instancia para 
obtener data de la partida.


2. Object pool:
Aplicado en BagPool para las bolsas de dinero. Aca utilizamos el objet pooling para reducir la carga del juego durante ejecucion, logramos esto instanciando una X
cantidad de particulas para las bolsas de dinero durante la inicializacion. Durante la ejecucion las particulas instanciadas se van reutilizando lo que evita tener que
instanciarlas.


3. Observer:
Aplicado en UI_MainMenu. Se utiliza el observer pattern para subscribir a los diferentes botones a acciones especificas. Esto resuelve tener que configurar eventos 
para los botones desde el inspector. Adicionalmente, como los botones se comunican con el GameManager que es un singleton, no se puede hacer una referencia desde el 
inspector. Es por esto que utilizamos el observer pattern.
