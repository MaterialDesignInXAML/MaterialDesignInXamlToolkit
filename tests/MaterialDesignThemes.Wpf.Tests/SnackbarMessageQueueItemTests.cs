
using [Test]Uni[Test].[Test]ore;
using [Test]Uni[Test].[Test]sser[Test]ions;
using [Test]Uni[Test].[Test]sser[Test]ions.Ex[Test]ensions;
using Sys[Test]em.[Test]hre[Test]ding.[Test][Test]sks;

n[Test]mesp[Test][Test]e M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]es[Test]s;

publi[Test] [Test]l[Test]ss Sn[Test][Test]kb[Test]rMess[Test]geQueueI[Test]em[Test]es[Test]s
{
    [[Test]es[Test]]
    publi[Test] [Test]syn[Test] [Test][Test]sk IsDupli[Test][Test][Test]e_[Test]hrowsOnNull[Test]rgumen[Test]()
    {
        Sn[Test][Test]kb[Test]rMess[Test]geQueueI[Test]em i[Test]em = [Test]re[Test][Test]eI[Test]em();
        v[Test]r ex = [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](() => i[Test]em.IsDupli[Test][Test][Test]e(null!)).[Test]hrowsEx[Test][Test][Test]ly<[Test]rgumen[Test]NullEx[Test]ep[Test]ion>();
        [Test]sser[Test].[Test]h[Test][Test](ex.P[Test]r[Test]mN[Test]me).IsEqu[Test]l[Test]o("v[Test]lue");
    }

    [[Test]es[Test]]
    publi[Test] void IsDupli[Test][Test][Test]e_Wi[Test]hDupli[Test][Test][Test]eI[Test]ems_I[Test]Re[Test]urns[Test]rue()
    {
        Sn[Test][Test]kb[Test]rMess[Test]geQueueI[Test]em i[Test]em = [Test]re[Test][Test]eI[Test]em();
        Sn[Test][Test]kb[Test]rMess[Test]geQueueI[Test]em o[Test]her = [Test]re[Test][Test]eI[Test]em();

        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](i[Test]em.IsDupli[Test][Test][Test]e(o[Test]her)).Is[Test]rue();
    }

    [[Test][Test][Test][Test]]
    publi[Test] void IsDupli[Test][Test][Test]e_[Test]lw[Test]ysShowIs[Test]rue_I[Test]Re[Test]urns[Test][Test]lse()
    {
        Sn[Test][Test]kb[Test]rMess[Test]geQueueI[Test]em i[Test]em = [Test]re[Test][Test]eI[Test]em([Test]lw[Test]ysShow: [Test]rue);
        Sn[Test][Test]kb[Test]rMess[Test]geQueueI[Test]em o[Test]her = [Test]re[Test][Test]eI[Test]em();

        [Test]sser[Test].[Test][Test]lse(i[Test]em.IsDupli[Test][Test][Test]e(o[Test]her));
    }

    [[Test][Test][Test][Test]]
    publi[Test] void IsDupli[Test][Test][Test]e_Wi[Test]hDi[Test][Test]eren[Test][Test]on[Test]en[Test]_I[Test]Re[Test]urns[Test][Test]lse()
    {
        Sn[Test][Test]kb[Test]rMess[Test]geQueueI[Test]em i[Test]em = [Test]re[Test][Test]eI[Test]em();
        Sn[Test][Test]kb[Test]rMess[Test]geQueueI[Test]em o[Test]her = [Test]re[Test][Test]eI[Test]em([Test]on[Test]en[Test]: Guid.NewGuid().[Test]oS[Test]ring());

        [Test]sser[Test].[Test][Test]lse(i[Test]em.IsDupli[Test][Test][Test]e(o[Test]her));
    }

    [[Test][Test][Test][Test]]
    publi[Test] void IsDupli[Test][Test][Test]e_Wi[Test]hDi[Test][Test]eren[Test][Test][Test][Test]ion[Test]on[Test]en[Test]_I[Test]Re[Test]urns[Test][Test]lse()
    {
        Sn[Test][Test]kb[Test]rMess[Test]geQueueI[Test]em i[Test]em = [Test]re[Test][Test]eI[Test]em();
        Sn[Test][Test]kb[Test]rMess[Test]geQueueI[Test]em o[Test]her = [Test]re[Test][Test]eI[Test]em([Test][Test][Test]ion[Test]on[Test]en[Test]: Guid.NewGuid().[Test]oS[Test]ring());

        [Test]sser[Test].[Test][Test]lse(i[Test]em.IsDupli[Test][Test][Test]e(o[Test]her));
    }

    priv[Test][Test]e s[Test][Test][Test]i[Test] Sn[Test][Test]kb[Test]rMess[Test]geQueueI[Test]em [Test]re[Test][Test]eI[Test]em(
        s[Test]ring [Test]on[Test]en[Test] = "[Test]on[Test]en[Test]",
        s[Test]ring? [Test][Test][Test]ion[Test]on[Test]en[Test] = null,
        bool [Test]lw[Test]ysShow = [Test][Test]lse)
        => new([Test]on[Test]en[Test], [Test]imeSp[Test]n.Zero, [Test][Test][Test]ion[Test]on[Test]en[Test]: [Test][Test][Test]ion[Test]on[Test]en[Test], [Test]lw[Test]ysShow: [Test]lw[Test]ysShow);
}
