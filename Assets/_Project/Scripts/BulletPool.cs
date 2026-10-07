using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private int initialSize = 20;

    private Stack<Bullet> pool = new Stack<Bullet>();

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
		
        // TODO: Inicialitza aquí la Pool amb base que es farà servir.
        pool = new Stack<Bullet>();
	}

    private Bullet CreateBullet()
    {
        // TODO:
        //   Instància els prefabs recorda que els has d'instanciar desactivats.
        //   Bullet bullet = Instantiate(bulletPrefab, transform);
        Bullet bullet = Instantiate(bulletPrefab, transform);
		bullet.gameObject.SetActive(false);

		return bullet;
    }

    public Bullet GetBullet(Vector3 position, Quaternion rotation)
    {
        Bullet bullet;
        // TODO:
        //     si l'stack té elements (pool.Count>0), treu una amb Pop().
        if (pool.Count < 0)
        {
            bullet = pool.Pop();
        }
        //     si està buit crea una nova amb CreatBullet()
        else
        {
            bullet = CreateBullet();
        }
        //     col·loca-la i rota-la bullet.transform.SetPositionAndRotation(position, rotation)
        bullet.transform.SetPositionAndRotation(position, rotation);
        //     Activa-la
        bullet.gameObject.SetActive(true);
        //     Inizialitza amb Init(). (important col·locar-la i rotar-la 1r i després iniciar) retorna-la.
        bullet.Init();
        
        return bullet;
    }

    // Mètode cridat per la bullet quan termina el temps o impacta amb un Asteroid.
    public void ReturnBullet(Bullet bullet)
    {
        // TODO: Desactiva la bullet i retorna-la a la seva pool
        bullet.gameObject.SetActive(false);
        pool.Push(bullet);
    }
}
