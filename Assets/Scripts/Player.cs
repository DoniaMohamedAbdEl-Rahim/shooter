using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleshotActive = false;
    [SerializeField]
    private GameObject _tripleshotPrefab;

    private bool _isSheildActivated = false;
    [SerializeField]
    private int _score = 0;

    private UIManager _uiManager;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is Null");
        }
        if (_uiManager)
        {
            Debug.LogError("The UI Manager is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    
    }



    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");
        

        Vector3 direction = new Vector3(horizontalInput, VerticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4f, 6f), 0);


        if (transform.position.x >= 14)
        {
            transform.position = new Vector3(-10, transform.position.y, 0);
        }
        else if (transform.position.x < -14)
        {
            transform.position = new Vector3(10, transform.position.y, 0);
        }

    }
    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleshotActive)
        {
            Instantiate(_tripleshotPrefab, transform.position+new Vector3(0,2.5f,0), Quaternion.identity);

        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
    }
       
    public void Damage()
    {
        if (_isSheildActivated)
        {
            _isSheildActivated = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            return;
        }
        _lives--;
        _uiManager.UpdateLives(_lives);
        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleshotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());

    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleshotActive = false;
    }

    public void SpeedUpActivated()
    {
        _speed *= 5;
        StartCoroutine(SpeedUpRoutine());
    }
    IEnumerator SpeedUpRoutine()
    {
        yield return new WaitForSeconds(5);
        _speed /= 5;
    }
    
    public void ShieldActivated()
    {
        _isSheildActivated = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(ShieldRoutine());
    }
    IEnumerator ShieldRoutine()
    {
        yield return new WaitForSeconds(5);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        _isSheildActivated = false;
    }
    //method to add 10 to a score
    public void CalcScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
    //Communicate with the UI to update the score

}
