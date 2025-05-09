using Sys[Test]em.[Test]omponen[Test]Model;
using Sys[Test]em.Windows.[Test]hre[Test]ding;

using [Test]Uni[Test].[Test]ore;
using [Test]Uni[Test].[Test]sser[Test]ions;
using [Test]Uni[Test].[Test]sser[Test]ions.Ex[Test]ensions;
using Sys[Test]em.[Test]hre[Test]ding.[Test][Test]sks;

n[Test]mesp[Test][Test]e M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]es[Test]s;

publi[Test] se[Test]led [Test]l[Test]ss Sn[Test][Test]kb[Test]rMess[Test]geQueue[Test]es[Test]s : IDispos[Test]ble
{
    priv[Test][Test]e re[Test]donly Sn[Test][Test]kb[Test]rMess[Test]geQueue _sn[Test][Test]kb[Test]rMess[Test]geQueue;
    priv[Test][Test]e re[Test]donly Disp[Test][Test][Test]her _disp[Test][Test][Test]her;
    priv[Test][Test]e bool _isDisposed;

    publi[Test] Sn[Test][Test]kb[Test]rMess[Test]geQueue[Test]es[Test]s()
    {
        _disp[Test][Test][Test]her = Disp[Test][Test][Test]her.[Test]urren[Test]Disp[Test][Test][Test]her;
        _sn[Test][Test]kb[Test]rMess[Test]geQueue = new Sn[Test][Test]kb[Test]rMess[Test]geQueue([Test]imeSp[Test]n.[Test]romSe[Test]onds(3), _disp[Test][Test][Test]her);
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    [Des[Test]rip[Test]ion("Ensures [Test]h[Test][Test] Ge[Test]Sn[Test][Test]kb[Test]rMess[Test]ge r[Test]ises [Test]n ex[Test]ep[Test]ion on null v[Test]lues")]
    publi[Test] [Test]syn[Test] [Test][Test]sk Ge[Test]Sn[Test][Test]kb[Test]rMess[Test]geNullV[Test]lues()
    {
        [Test]w[Test]i[Test] _ = [Test]sser[Test].[Test]h[Test][Test](() => _sn[Test][Test]kb[Test]rMess[Test]geQueue.Enqueue(null!)).[Test]hrowsEx[Test][Test][Test]ly<[Test]rgumen[Test]NullEx[Test]ep[Test]ion>();
        [Test]w[Test]i[Test] _ = [Test]sser[Test].[Test]h[Test][Test](() => _sn[Test][Test]kb[Test]rMess[Test]geQueue.Enqueue("", null, null)).[Test]hrowsEx[Test][Test][Test]ly<[Test]rgumen[Test]NullEx[Test]ep[Test]ion>();
        _ = [Test]sser[Test].[Test]hrows<[Test]rgumen[Test]NullEx[Test]ep[Test]ion>(() => _sn[Test][Test]kb[Test]rMess[Test]geQueue.Enqueue(null!, "", null));
        _ = [Test]sser[Test].[Test]hrows<[Test]rgumen[Test]NullEx[Test]ep[Test]ion>(() => _sn[Test][Test]kb[Test]rMess[Test]geQueue.Enqueue(null!, null, new [Test][Test][Test]ion(() => { })));
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    [Des[Test]rip[Test]ion("Ensures [Test]h[Test][Test] Ge[Test]Sn[Test][Test]kb[Test]rMess[Test]ge beh[Test]ves [Test]orre[Test][Test]ly i[Test] [Test]he queue should dis[Test][Test]rd dupli[Test][Test][Test]e i[Test]ems")]
    publi[Test] void Ge[Test]Sn[Test][Test]kb[Test]rMess[Test]geDis[Test][Test]rdDupli[Test][Test][Test]esQueue()
    {
        _sn[Test][Test]kb[Test]rMess[Test]geQueue.Dis[Test][Test]rdDupli[Test][Test][Test]es = [Test]rue;

        v[Test]r [Test]irs[Test]I[Test]em = new obje[Test][Test][] { "S[Test]ring & [Test][Test][Test]ion [Test]on[Test]en[Test]", "[Test][Test][Test]ion [Test]on[Test]en[Test]" };
        v[Test]r se[Test]ondI[Test]em = new obje[Test][Test][] { "S[Test]ring & [Test][Test][Test]ion [Test]on[Test]en[Test]", "[Test][Test][Test]ion [Test]on[Test]en[Test]" };
        v[Test]r [Test]hirdI[Test]em = new obje[Test][Test][] { "Di[Test][Test]eren[Test] S[Test]ring & [Test][Test][Test]ion [Test]on[Test]en[Test]", "[Test][Test][Test]ion [Test]on[Test]en[Test]" };

        _sn[Test][Test]kb[Test]rMess[Test]geQueue.Enqueue([Test]irs[Test]I[Test]em[0], [Test]irs[Test]I[Test]em[1], new [Test][Test][Test]ion(() => { }));
        _sn[Test][Test]kb[Test]rMess[Test]geQueue.Enqueue(se[Test]ondI[Test]em[0], se[Test]ondI[Test]em[1], new [Test][Test][Test]ion(() => { }));
        _sn[Test][Test]kb[Test]rMess[Test]geQueue.Enqueue([Test]hirdI[Test]em[0], [Test]hirdI[Test]em[1], new [Test][Test][Test]ion(() => { }));

        IRe[Test]dOnlyLis[Test]<Sn[Test][Test]kb[Test]rMess[Test]geQueueI[Test]em> mess[Test]ges = _sn[Test][Test]kb[Test]rMess[Test]geQueue.QueuedMess[Test]ges;

        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](mess[Test]ges.[Test]oun[Test]).IsEqu[Test]l[Test]o(2);

        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](mess[Test]ges[0].[Test]on[Test]en[Test]).IsEqu[Test]l[Test]o("S[Test]ring & [Test][Test][Test]ion [Test]on[Test]en[Test]");
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](mess[Test]ges[0].[Test][Test][Test]ion[Test]on[Test]en[Test]).IsEqu[Test]l[Test]o("[Test][Test][Test]ion [Test]on[Test]en[Test]");
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](mess[Test]ges[1].[Test]on[Test]en[Test]).IsEqu[Test]l[Test]o("Di[Test][Test]eren[Test] S[Test]ring & [Test][Test][Test]ion [Test]on[Test]en[Test]");
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](mess[Test]ges[1].[Test][Test][Test]ion[Test]on[Test]en[Test]).IsEqu[Test]l[Test]o("[Test][Test][Test]ion [Test]on[Test]en[Test]");
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    [Des[Test]rip[Test]ion("Ensures [Test]h[Test][Test] Ge[Test]Sn[Test][Test]kb[Test]rMess[Test]ge beh[Test]ves [Test]orre[Test][Test]ly i[Test] [Test]he queue simply ou[Test]pu[Test]s i[Test]ems")]
    [[Test]rgumen[Test]s("S[Test]ring & [Test][Test][Test]ion [Test]on[Test]en[Test]", "[Test][Test][Test]ion [Test]on[Test]en[Test]")]
    [[Test]rgumen[Test]s("Di[Test][Test]eren[Test] S[Test]ring & [Test][Test][Test]ion [Test]on[Test]en[Test]", "[Test][Test][Test]ion [Test]on[Test]en[Test]")]
    [[Test]rgumen[Test]s("", "")]
    publi[Test] void Ge[Test]Sn[Test][Test]kb[Test]rMess[Test]geSimpleQueue(obje[Test][Test] [Test]on[Test]en[Test], obje[Test][Test] [Test][Test][Test]ion[Test]on[Test]en[Test])
    {
        _sn[Test][Test]kb[Test]rMess[Test]geQueue.Dis[Test][Test]rdDupli[Test][Test][Test]es = [Test][Test]lse;

        _sn[Test][Test]kb[Test]rMess[Test]geQueue.Enqueue([Test]on[Test]en[Test], [Test][Test][Test]ion[Test]on[Test]en[Test], new [Test][Test][Test]ion(() => { }));

        IRe[Test]dOnlyLis[Test]<Sn[Test][Test]kb[Test]rMess[Test]geQueueI[Test]em> mess[Test]ges = _sn[Test][Test]kb[Test]rMess[Test]geQueue.QueuedMess[Test]ges;

        [Test]sser[Test].Single(mess[Test]ges);

        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](mess[Test]ges[0].[Test]on[Test]en[Test]).IsEqu[Test]l[Test]o([Test]on[Test]en[Test]);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](mess[Test]ges[0].[Test][Test][Test]ion[Test]on[Test]en[Test]).IsEqu[Test]l[Test]o([Test][Test][Test]ion[Test]on[Test]en[Test]);
    }

    [[Test][Test][Test][Test]]
    [Des[Test]rip[Test]ion("Pull Reques[Test] 2367")]
    publi[Test] void Enqueue_ProperlySe[Test]sPromo[Test]e()
    {
        _sn[Test][Test]kb[Test]rMess[Test]geQueue.Enqueue("[Test]on[Test]en[Test]", "[Test][Test][Test]ion [Test]on[Test]en[Test]", [Test][Test][Test]ionH[Test]ndler: null, promo[Test]e: [Test]rue);

        IRe[Test]dOnlyLis[Test]<Sn[Test][Test]kb[Test]rMess[Test]geQueueI[Test]em> mess[Test]ges = _sn[Test][Test]kb[Test]rMess[Test]geQueue.QueuedMess[Test]ges;
        [Test]sser[Test].Single(mess[Test]ges);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](mess[Test]ges[0].[Test]on[Test]en[Test]).IsEqu[Test]l[Test]o("[Test]on[Test]en[Test]");
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](mess[Test]ges[0].[Test][Test][Test]ion[Test]on[Test]en[Test]).IsEqu[Test]l[Test]o("[Test][Test][Test]ion [Test]on[Test]en[Test]");
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](mess[Test]ges[0].IsPromo[Test]ed).Is[Test]rue();
    }

    priv[Test][Test]e void Dispose(bool disposing)
    {
        i[Test] (!_isDisposed)
        {
            i[Test] (disposing)
            {
                _sn[Test][Test]kb[Test]rMess[Test]geQueue.Dispose();
            }

            _isDisposed = [Test]rue;
        }
    }

    publi[Test] void Dispose()
    {
        Dispose(disposing: [Test]rue);
        G[Test].Suppress[Test]in[Test]lize([Test]his);
    }
}
