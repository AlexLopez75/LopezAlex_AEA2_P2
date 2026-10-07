using System.Collections.Generic;
using UnityEngine;

// TODO: la mateixa estructura que el bullet pool.
public class AsteroidPool : MonoBehaviour
{
    public static AsteroidPool Instance;

    [SerializeField] private Asteroid asteroidPrefab;
    [SerializeField] private int initialSize = 15;

    private Stack<Asteroid> pool = new Stack<Asteroid>();

    private void Awake()
    {
        // TODO:
        //   Singleton.
        //   Fer les comprovacions necessàries perquè només hi hagi una instància d'aquest singleton 
        if (Instance == null && Instance != this)
		{ 
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);

        // TODO:Inicialitza aquí la Pool amb base que es farà servir. 
        pool = new Stack<Asteroid>();
    }

    private Asteroid CreateAsteroid()
    {
		// TODO:
		//   Instància els prefabs recorda que els has d'instanciar desactivats.
		Asteroid asteroid = Instantiate(asteroidPrefab, transform);
		asteroid.gameObject.SetActive(false);

        //   Fixeu-vos que el mètode ha de retornar un Asteroid
        return asteroid;
    }

    public Asteroid GetAsteroid(Vector3 position)
    {
		// TODO:
		//   si l'stack no esta buit (pool.Count > 0), treu un Asteroid Pop().
        Asteroid asteroid;

		if (pool.Count > 0)
        {
            asteroid = pool.Pop();
        }
        //   si no en queda cap instancia un CreateAsteroid().
        else
        {
            asteroid = CreateAsteroid();
        }
		//   col·loca en la posició correcta, activa'l i retorna'l
		asteroid.transform.position = position;
		asteroid.gameObject.SetActive(true);

		return asteroidPrefab;
    }

    public void ReturnAsteroid(Asteroid asteroid)
    {
        // TODO:
        //   Desactiva l'asteroid i torna'l al stack Push().
        asteroid.gameObject.SetActive(false);
        pool.Push(asteroid);
    }
}
