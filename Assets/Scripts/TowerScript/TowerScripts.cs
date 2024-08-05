using UnityEngine;

public class TowerScripts : MonoBehaviour
{
    private bool isActivate = false;

    //Canvas
    [SerializeField] private GameObject _gunSelectionCanvas;
    [SerializeField] private GameObject _gunUpgradeCanvas;

    //Tower
    [SerializeField] private GameObject _towerActive;
    [SerializeField] private Transform _cannon;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] cannonImage;

    //Shoot
    public float range = 5f;
    public float damage = 10f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public GameObject bulletPrefab;
    public GameObject rocketPrefab;
    public Transform firePoint;


    private void Awake()
    {
        _gunSelectionCanvas.SetActive(false);
        _gunUpgradeCanvas.SetActive(false);
        _towerActive.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (isActivate == false)
        {
            _gunSelectionCanvas.SetActive(true);
        }
        else
        {
            _gunUpgradeCanvas.SetActive(true);
        }

    }

    public void SelectGun(int canonSP)
    {
        switch (canonSP)
        {
            case 1:
                if (GameManager.Instance.money > 2000)
                {
                    GameManager.Instance.money -= 2000; 
                    range = range * 2;
                    damage = damage * 0.2f;
                    fireRate = fireRate * 2f;

                    canonType = CanonType.Normal;
                    _towerActive.SetActive(true);
                    spriteRenderer.sprite = cannonImage[0];
                    _gunSelectionCanvas.SetActive(false);
                    isActivate = true;
                }

                break;
            case 2:
                if (GameManager.Instance.money > 5000)
                {
                    GameManager.Instance.money -= 5000;
                    damage = damage * 5f;
                    fireRate = fireRate * 0.4f;
                    canonType = CanonType.Special;
                    _towerActive.SetActive(true);
                    spriteRenderer.sprite = cannonImage[1];
                    _gunSelectionCanvas.SetActive(false);
                    isActivate = true;
                }

                break;
        }
    }



    public enum CanonType
    {
        Normal,
        Special
    }

    public CanonType canonType;



    void Update()
    {
        fireCountdown -= Time.deltaTime;
        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            RotateTowards(nearestEnemy.transform);
            if (fireCountdown <= 0f)
            {
                Shoot(nearestEnemy.transform);
                fireCountdown = 1f / fireRate;
            }
        }
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    private void Shoot(Transform target)
    {
        if (!isActivate) return;

        if (Vector2.Distance(transform.position, target.position) < range)
        {

            GameObject bulletGO = null;
            if (canonType == CanonType.Normal)
            {
                bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
            else
            {
                bulletGO = Instantiate(rocketPrefab, firePoint.position, _cannon.rotation);
            }

            Bullet bullet = bulletGO.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.damage = damage;
                bullet.Seek(target);
            }
        }
    }


    private void RotateTowards(Transform target)
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _cannon.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }


    public void CloseUpgradePanel()
    {
        _gunUpgradeCanvas.SetActive(false);
        _gunSelectionCanvas.SetActive(false);
    }

    public void GunUpgrade()
    {
        if (canonType == CanonType.Special)
        {
            if (GameManager.Instance.money > 1000)
            {
                GameManager.Instance.money -= 1000;

                range += range*0.2f;
                damage += damage*0.2f;
                fireRate += fireRate*0.2f;
            }
        }

        if (canonType == CanonType.Normal)
        {
            if (GameManager.Instance.money > 500)
            {
                GameManager.Instance.money -= 1000;
                range += range * 0.2f;
                damage += damage * 0.2f;
                fireRate += fireRate * 0.2f;
            }
        }
        CloseUpgradePanel();
    }
}
