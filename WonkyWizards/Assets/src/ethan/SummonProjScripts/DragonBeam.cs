public class DragonBeam : SummonProj
{
    public DragonBeam()
    {
        speed = 0;
        damage = 5;
        kockback = 0f;
    }

    public override void Start()
    {
        lookAt2D(target);

        Invoke("killSelf", 0.05f);
    }

    public override void FixedUpdate()
    {
        // we don't want the default "move toward" behaviour,
        // so do not even call 'base.FixedUpdate()'.
    }
}
