using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _triplelaserPrefab;

    [SerializeField]
    private float _fireRate = 0.15f;
    [SerializeField]
    private float _canFire = -1f;

    [SerializeField]
    private float _lives = 3;
    [SerializeField]
    private GameObject gameOverPanel;
    private SpawnManager _spawnManager;

    [SerializeField]
    private bool _isTripleShotActive = false;

    [SerializeField]
    private bool _isSpeedActive = false;

    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private Transform _shieldSpawnPosition;
    [SerializeField]
    private GameObject _shieldObject;




    void Start()
    {
        _shieldObject.SetActive(false);
        gameOverPanel.SetActive(false);
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }


    void Update()
    {
        Movement();
        Speed();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate (direction * _speed * Time.deltaTime);

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }

        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }
        

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }

        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
        
    }

    void Speed()
    {
        if (_isSpeedActive == true)
        {
            _speed = 7f;
        }
        else
        {
            _speed = 3.5f;
        }
    }

    void FireLaser ()
    {
        _canFire = Time.time + _fireRate;

        if(_isTripleShotActive == true)
        {
            Instantiate(_triplelaserPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        }
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {

        }
        else if (_isShieldActive == false)
        {
            _lives--;

            if (_lives < 1)
            {
                gameOverPanel.SetActive(true);
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
            }
        }
    }
        

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isTripleShotActive = false;
    }

    public void SpeedActive()
    {
        _isSpeedActive = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isSpeedActive = false;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldObject.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutine());
    }

    IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isShieldActive = false;
        _shieldObject.SetActive(false);
    }
}
