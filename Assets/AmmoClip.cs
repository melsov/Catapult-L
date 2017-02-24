using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class AmmoClip : Singleton<AmmoClip> {
    protected AmmoClip() { }

    public float refillInterval = 8f;
    public int refillAmount = 4;
    public RectTransform percentageBarRT;
    public RectTransform percentageBarRTMobile;
    private PercentageBar _percentageBar;
    private PercentageBar percentageBar {
        get {
            if (!_percentageBar) {
#if UNITY_IOS || UNITY_ANDROID
                _percentageBar = percentageBarRTMobile.GetComponentInChildren<PercentageBar>();
#else
                _percentageBar = percentageBarRT.GetComponentInChildren<PercentageBar>();
#endif
            }
            return _percentageBar;
        }
    }

    public int maxAmmo = 50;
    private int _ammo;
    private int ammo {
        get { return _ammo; }
        set {
            if(value > _ammo) {
                percentageBar.glow();
            }
            _ammo = value;
            percentageBar.set(percentageAmmo);
            percentageBar.setNumbers(_ammo, maxAmmo);
        }
    }

    protected float percentageAmmo {
        get {
            return (float)_ammo / (float)maxAmmo;
        }
    }

	public void Awake () {
        setup();
        //StartCoroutine(refill());
	}

    //private IEnumerator refill() {
    //    while(true) {
    //        yield return new WaitForSeconds(refillInterval);
    //        ammo += refillAmount;
    //    }
    //}

    public void OnEnable() {
        ScoreKeeper.Instance.OnReset += setup;
    }
    public void OnDisable() {
        if (ScoreKeeper.Instance && ScoreKeeper.Instance.OnReset != null) {
            ScoreKeeper.Instance.OnReset -= setup;
        }
    }

    private void setup() {
        ammo = maxAmmo;
    }

    private bool available {
        get { return ammo > 0; }
    }

    public bool deductAmmo() {
        if (available) {
            ammo--;
            return true;
        }
        return false;
    }

    public int addAmmo(int amt) {
        int t = ammo + amt;
        ammo = Mathf.Clamp(t, 0, maxAmmo);
        return Mathf.Max(0, t - maxAmmo);
    }

    public void resetAmmo() {
        ammo = maxAmmo;
    }
	
	void Update () {
	
	}
}
